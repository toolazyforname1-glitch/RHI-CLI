using System.IO.Pipes;
using System.Text.Json;
using Microsoft.UI.Dispatching;

namespace RenoDXCommander.Cli;

/// <summary>
/// Request/response transport to a running RHI. The CLI sends one JSON array of arguments and gets
/// back the command's result; the GUI runs the command on its UI thread through the same registry
/// the headless path uses, so a terminal command and a click take the same code path.
/// </summary>
internal static class CliPipe
{
    private const string PipeName = "RenoDXCommander_CliPipe";
    private static readonly TimeSpan ClientTimeout = TimeSpan.FromMilliseconds(750);
    private static readonly TimeSpan PipeTimeout = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan ServerReadTimeout = TimeSpan.FromSeconds(15);

    private static string? ReadLineWithTimeout(TextReader reader, TimeSpan timeout)
    {
        var read = Task.Run(reader.ReadLine);
        try
        {
            return read.Wait(timeout) ? read.Result : null;
        }
        catch (Exception ex) when (ex is AggregateException or IOException or ObjectDisposedException)
        {
            return null;
        }
    }
    private static bool _serverStarted;

    internal static bool TrySend(string[] args, out string response, out int exitCode)
    {
        response = "";
        exitCode = 1;

        try
        {
            using var client = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
            client.Connect((int)ClientTimeout.TotalMilliseconds);

            using var writer = new StreamWriter(client, System.Text.Encoding.UTF8, 4096, leaveOpen: true) { AutoFlush = true };
            using var reader = new StreamReader(client, System.Text.Encoding.UTF8, false, 4096, leaveOpen: true);

            writer.WriteLine(JsonSerializer.Serialize(args));
            var line = ReadLineWithTimeout(reader, PipeTimeout);
            if (string.IsNullOrWhiteSpace(line)) return false;

            response = line;
            using var document = JsonDocument.Parse(line);
            if (document.RootElement.TryGetProperty("exitCode", out var code)) exitCode = code.GetInt32();
            return true;
        }
        catch (Exception ex) when (ex is IOException or TimeoutException or OperationCanceledException
                                       or UnauthorizedAccessException or JsonException)
        {
            return false;
        }
    }

    internal static void StartServer(DispatcherQueue dispatcher)
    {
        if (_serverStarted) return;
        _serverStarted = true;
        var thread = new Thread(() => ListenLoop(dispatcher)) { IsBackground = true, Name = "RHI CLI pipe" };
        thread.Start();
    }

    private static void ListenLoop(DispatcherQueue dispatcher)
    {
        while (true)
        {
            try
            {
                using var server = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1,
                    PipeTransmissionMode.Byte, PipeOptions.None);
                server.WaitForConnection();

                using var reader = new StreamReader(server, System.Text.Encoding.UTF8, false, 4096, leaveOpen: true);
                using var writer = new StreamWriter(server, System.Text.Encoding.UTF8, 4096, leaveOpen: true) { AutoFlush = true };

                var line = ReadLineWithTimeout(reader, ServerReadTimeout);
                if (string.IsNullOrWhiteSpace(line)) continue;

                var args = JsonSerializer.Deserialize<string[]>(line);
                if (args is null || args.Length == 0) { writer.WriteLine(ErrorEnvelope("empty request")); continue; }

                var payload = Run(dispatcher, args);
                writer.WriteLine(payload);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Thread.Sleep(250);
            }
            catch (JsonException)
            {
                Thread.Sleep(50);
            }
            catch (Exception)
            {
                // The listener must outlive a bad client: one failed exchange cannot kill the channel.
                Thread.Sleep(250);
            }
        }
    }

    private static string Run(DispatcherQueue dispatcher, string[] args)
    {
        var completion = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!dispatcher.TryEnqueue(async () =>
            {
                try
                {
                    var (name, result) = await CliHost.ExecuteInProcessAsync(args);
                    completion.TrySetResult(JsonSerializer.Serialize(new
                    {
                        ok = result.ExitCode == 0,
                        command = name,
                        exitCode = result.ExitCode,
                        message = result.Message,
                        data = result.Data,
                    }));
                }
                catch (Exception ex)
                {
                    completion.TrySetResult(ErrorEnvelope($"{ex.GetType().Name}: {ex.Message}"));
                }
            }))
        {
            return ErrorEnvelope("RHI's UI thread is not accepting work right now");
        }

        return completion.Task.Wait(PipeTimeout)
            ? completion.Task.Result
            : ErrorEnvelope("the command did not finish in 10 minutes - it may still be running in RHI");
    }

    private static string ErrorEnvelope(string message) =>
        JsonSerializer.Serialize(new { ok = false, exitCode = 1, message, data = (object?)null });
}
