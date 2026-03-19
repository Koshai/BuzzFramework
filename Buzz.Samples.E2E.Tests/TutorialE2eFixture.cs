using System.Diagnostics;
using System.Net.Http;
using Microsoft.Playwright;

namespace Buzz.Samples.E2E.Tests;

[CollectionDefinition(CollectionName)]
public sealed class TutorialE2eCollection : ICollectionFixture<TutorialE2eFixture>
{
    public const string CollectionName = "Tutorial E2E";
}

public sealed class TutorialE2eFixture : IAsyncLifetime
{
    private const string BaseUrl = "http://127.0.0.1:5127";
    private static readonly TimeSpan StartupTimeout = TimeSpan.FromSeconds(60);

    private readonly HttpClient _httpClient = new();
    private readonly List<string> _serverLogs = [];
    private Process? _serverProcess;
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    public string SiteBaseUrl => BaseUrl;

    public async Task InitializeAsync()
    {
        await EnsureChromiumInstalledAsync();

        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        await StartSampleSiteAsync();
        await WaitForSiteReadyAsync();
    }

    public async Task DisposeAsync()
    {
        if (_browser is not null)
        {
            await _browser.CloseAsync();
        }

        _playwright?.Dispose();

        if (_serverProcess is { HasExited: false })
        {
            _serverProcess.Kill(entireProcessTree: true);
            _serverProcess.Dispose();
        }

        _httpClient.Dispose();
    }

    public async Task<IPage> NewPageAsync()
    {
        if (_browser is null)
        {
            throw new InvalidOperationException("Browser is not initialized.");
        }

        var context = await _browser.NewContextAsync();
        return await context.NewPageAsync();
    }

    private static Task EnsureChromiumInstalledAsync()
    {
        var exitCode = Microsoft.Playwright.Program.Main(["install", "chromium"]);
        if (exitCode != 0)
        {
            throw new InvalidOperationException("Playwright browser installation failed.");
        }

        return Task.CompletedTask;
    }

    private async Task StartSampleSiteAsync()
    {
        var solutionRoot = GetSolutionRoot();
        var sampleProject = Path.Combine(solutionRoot, "Buzz.Samples", "Buzz.Samples.csproj");
        var runtimeOutput = Path.Combine(solutionRoot, ".artifacts", "e2e-runtime", "buzz-samples");

        Directory.CreateDirectory(runtimeOutput);
        await BuildSampleAppAsync(solutionRoot, sampleProject, runtimeOutput);

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"\"{Path.Combine(runtimeOutput, "Buzz.Samples.dll")}\" --urls {BaseUrl}",
            WorkingDirectory = solutionRoot,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        _serverProcess = new Process { StartInfo = startInfo };
        _serverProcess.OutputDataReceived += (_, args) => AppendServerLog(args.Data);
        _serverProcess.ErrorDataReceived += (_, args) => AppendServerLog(args.Data);

        if (!_serverProcess.Start())
        {
            throw new InvalidOperationException("Failed to start Buzz.Samples for E2E tests.");
        }

        _serverProcess.BeginOutputReadLine();
        _serverProcess.BeginErrorReadLine();
    }

    private async Task BuildSampleAppAsync(string solutionRoot, string sampleProject, string runtimeOutput)
    {
        var buildLogs = new List<string>();
        var buildInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"build \"{sampleProject}\" -o \"{runtimeOutput}\"",
            WorkingDirectory = solutionRoot,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using var buildProcess = new Process { StartInfo = buildInfo };
        buildProcess.OutputDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                buildLogs.Add(args.Data);
            }
        };
        buildProcess.ErrorDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                buildLogs.Add(args.Data);
            }
        };

        if (!buildProcess.Start())
        {
            throw new InvalidOperationException("Could not start dotnet build for Buzz.Samples.");
        }

        buildProcess.BeginOutputReadLine();
        buildProcess.BeginErrorReadLine();
        await buildProcess.WaitForExitAsync();

        if (buildProcess.ExitCode != 0)
        {
            throw new InvalidOperationException(
                $"Failed to build Buzz.Samples for E2E runtime. Build logs:{Environment.NewLine}{string.Join(Environment.NewLine, buildLogs)}");
        }
    }

    private async Task WaitForSiteReadyAsync()
    {
        var start = DateTimeOffset.UtcNow;
        var lastError = string.Empty;

        while (DateTimeOffset.UtcNow - start < StartupTimeout)
        {
            if (_serverProcess is { HasExited: true })
            {
                throw new InvalidOperationException(
                    $"Buzz.Samples exited during startup. Logs:{Environment.NewLine}{string.Join(Environment.NewLine, _serverLogs)}");
            }

            try
            {
                using var response = await _httpClient.GetAsync(BaseUrl);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                lastError = $"Unexpected status code: {(int)response.StatusCode}";
            }
            catch (Exception ex)
            {
                lastError = ex.Message;
            }

            await Task.Delay(500);
        }

        throw new TimeoutException(
            $"Timed out waiting for Buzz.Samples to start. Last error: {lastError}{Environment.NewLine}" +
            $"Server logs:{Environment.NewLine}{string.Join(Environment.NewLine, _serverLogs)}");
    }

    private static string GetSolutionRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "BuzzFramework.sln");
            if (File.Exists(candidate))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Could not locate BuzzFramework.sln from test runtime path.");
    }

    private void AppendServerLog(string? line)
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            _serverLogs.Add(line);
        }
    }
}
