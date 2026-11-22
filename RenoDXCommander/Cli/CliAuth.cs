using System.Text;
using System.Text.Json;
using RenoDXCommander.Services;

namespace RenoDXCommander.Cli;

/// <summary>
/// Account state, plus storing a GitHub personal access token. GitHub is a pasted token in the GUI,
/// so the CLI accepts one too - from an environment variable only (never argv, which lands in shell
/// history and process lists), and only after GitHub confirms it. Nexus signs in through an
/// interactive SSO websocket, so that stays in RHI's Settings page. No command prints a secret.
/// </summary>
internal static class CliAuth
{
    internal static readonly CliCommand[] All = [StatusCommand(), GitHubCommand(), NexusCommand()];

    private static CliCommand StatusCommand() => new()
    {
        Name = "auth status",
        Group = "auth",
        Summary = "Show whether GitHub and Nexus are connected (never prints the stored secrets).",
        Usage = "rhi auth status [--json]",
        Examples = ["rhi auth status", "rhi auth status --json"],
        Run = _ =>
        {
            var settings = SettingsFile.Load();

            var githubUser = Value(settings, "GitHubUsername");
            var githubToken = Configured(settings, "GitHubOAuthToken");
            var nexusUser = Value(settings, "NexusUsername");
            var nexusKey = Configured(settings, "NexusApiKey");

            var text = new StringBuilder();
            text.AppendLine("github   " + (githubUser.Length > 0 ? githubUser : "(not connected)") + (githubToken ? "   token stored" : ""));
            text.AppendLine("nexus    " + (nexusUser.Length > 0 ? nexusUser : "(not connected)") + (nexusKey ? "   api key stored" : ""));
            text.AppendLine();
            text.Append("sign in from RHI's Settings page - both logins are interactive");

            return CliResult.Ok(text.ToString(), new
            {
                github = new { username = githubUser, configured = githubUser.Length > 0 || githubToken, tokenStored = githubToken },
                nexus = new { username = nexusUser, configured = nexusUser.Length > 0 || nexusKey, apiKeyStored = nexusKey },
            });
        },
    };

    private static CliCommand GitHubCommand() => new()
    {
        Name = "auth github",
        Group = "auth",
        Summary = "Store a GitHub token taken from an environment variable, after GitHub confirms it.",
        Usage = "set GH_TOKEN=<token> && rhi auth github [--from-env NAME] [--yes] [--json]   |   rhi auth github --clear --yes",
        Examples = ["set GH_TOKEN=ghp_xxx && rhi auth github --yes", "rhi auth github --clear --yes"],
        Destructive = true,
        Run = invocation => GitHubToken(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> GitHubToken(CliInvocation invocation)
    {
        if (invocation.Flag("token") is not null)
            return CliResult.Usage("refusing to take a token as an argument - it would be recorded in shell history. Use: set GH_TOKEN=<token> && rhi auth github");

        if (invocation.Has("clear"))
        {
            if (!invocation.Confirmed)
                return CliResult.Usage("refusing to clear credentials without --yes: rhi auth github --clear --yes");

            if (!SettingsFile.Update(store =>
                {
                    store["GitHubOAuthToken"] = "";
                    store["GitHubUsername"] = "";
                }, "cli:auth clear"))
                return CliResult.Fail("could not write the settings file");

            return CliResult.Ok("GitHub token cleared", new { github = new { configured = false, tokenStored = false } });
        }

        var variable = invocation.Flag("from-env") ?? "GH_TOKEN";
        var token = Environment.GetEnvironmentVariable(variable)?.Trim() ?? "";

        if (token.Length == 0)
            return CliResult.Fail($"{variable} is not set - run: set {variable}=<token> && rhi auth github");

        if (!token.StartsWith("ghp_", StringComparison.OrdinalIgnoreCase) &&
            !token.StartsWith("github_pat_", StringComparison.OrdinalIgnoreCase))
            return CliResult.Fail("a GitHub token starts with ghp_ or github_pat_ - nothing was saved");

        if (invocation.DryRun)
            return CliResult.Ok($"would validate the {variable} token and store it", new { github = new { configured = false }, dryRun = true });

        var login = await ValidateAsync(token);
        if (login is null) return CliResult.Fail("GitHub rejected the token - nothing was saved");

        if (!SettingsFile.Update(store =>
            {
                store["GitHubOAuthToken"] = token;
                store["GitHubUsername"] = login;
            }, "cli:auth github"))
            return CliResult.Fail("could not write the settings file");

        return CliResult.Ok($"GitHub token stored for '{login}'. Takes effect the next time RHI reads its settings.",
            new { github = new { username = login, configured = true, tokenStored = true } });
    }

    private static CliCommand NexusCommand() => new()
    {
        Name = "auth nexus",
        Group = "auth",
        Summary = "Sign in to Nexus through its SSO page and store the API key.",
        Usage = "rhi auth nexus [--open] [--timeout 120] [--json]",
        Examples = ["rhi auth nexus --open"],
        Run = invocation => NexusSignIn(invocation).GetAwaiter().GetResult(),
    };

    private static async Task<CliResult> NexusSignIn(CliInvocation invocation)
    {
        if (invocation.Json)
            return CliResult.Usage("rhi auth nexus prints a URL you have to open in a browser, so it cannot run with --json - run it without --json");

        var timeout = 120;
        if (invocation.Flag("timeout") is { } raw && (!int.TryParse(raw, out timeout) || timeout is < 15 or > 600))
            return CliResult.Usage("--timeout must be between 15 and 600 seconds");

        var sso = CliServices.Get<NexusSsoService>();
        var downloader = CliServices.Get<NexusDownloadService>();
        var openBrowser = invocation.Has("open");

        void OnUrl(string url)
        {
            CliConsole.WriteLine("Authorise this sign-in in your browser:");
            CliConsole.WriteLine("  " + url);

            if (!openBrowser) return;
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception)
            {
                CliConsole.WriteLine("could not open the browser - copy the URL above");
            }
        }

        string? key;
        sso.BrowserUrlReady += OnUrl;
        try
        {
            key = await sso.AuthoriseAsync(CancellationToken.None, timeout);
        }
        finally
        {
            sso.BrowserUrlReady -= OnUrl;
        }

        if (string.IsNullOrEmpty(key))
            return CliResult.Fail($"no authorisation after {timeout}s - nothing was saved; try again with: rhi auth nexus --open");

        var info = await downloader.ValidateApiKeyAsync(key);
        if (info is null) return CliResult.Fail("Nexus rejected the key - nothing was saved");

        if (!SettingsFile.Update(store =>
            {
                store["NexusApiKey"] = key;
                store["NexusUsername"] = info.Name;
            }, "cli:auth nexus"))
            return CliResult.Fail("could not write the settings file");

        return CliResult.Ok(
            $"Nexus connected as '{info.Name}'{(info.IsPremium ? " (premium)" : "")}. Takes effect the next time RHI reads its settings.",
            new { nexus = new { username = info.Name, premium = info.IsPremium, configured = true } });
    }

    private static async Task<string?> ValidateAsync(string token)
    {
        try
        {
            var http = CliServices.Get<HttpClient>();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user");
            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
            request.Headers.TryAddWithoutValidation("User-Agent", "RHI");
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cts.Token);
            if (!response.IsSuccessStatusCode) return null;

            using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync(cts.Token));
            return document.RootElement.TryGetProperty("login", out var login) ? login.GetString() : null;
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException)
        {
            return null;
        }
    }

    private static string Value(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) ? value ?? "" : "";

    private static bool Configured(Dictionary<string, string> settings, string key) =>
        settings.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value);
}
