using System.Text.Json;
using Buzz.Blazor.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Buzz.Blazor.Services;

internal sealed class JsonBuzzSeedKnowledgeStore : IBuzzSeedKnowledgeStore
{
    private sealed record SeedPayload(IReadOnlyList<BuzzSeedKnowledgeEntry>? Entries);

    private readonly ILogger<JsonBuzzSeedKnowledgeStore> _logger;
    private readonly BuzzOptions _options;
    private readonly SemaphoreSlim _loadGate = new(1, 1);
    private IReadOnlyList<BuzzSeedKnowledgeEntry> _entries = [];
    private bool _isLoaded;

    public JsonBuzzSeedKnowledgeStore(
        IOptions<BuzzOptions> options,
        ILogger<JsonBuzzSeedKnowledgeStore> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task WarmupAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<BuzzSeedKnowledgeEntry>> SearchAsync(
        string subject,
        string? component,
        string query,
        int maxResults,
        CancellationToken cancellationToken = default)
    {
        if (maxResults <= 0 || string.IsNullOrWhiteSpace(subject))
        {
            return [];
        }

        await EnsureLoadedAsync(cancellationToken);
        if (_entries.Count == 0)
        {
            return [];
        }

        var subjectKey = subject.Trim();
        var componentKey = component?.Trim() ?? string.Empty;
        var queryKey = query?.Trim() ?? string.Empty;

        var ranked = _entries
            .Where(entry => entry.Subject.Equals(subjectKey, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(entry => Score(entry, componentKey, queryKey))
            .ThenBy(entry => entry.Title, StringComparer.OrdinalIgnoreCase)
            .Take(maxResults)
            .ToList();

        return ranked;
    }

    private async Task EnsureLoadedAsync(CancellationToken cancellationToken)
    {
        if (_isLoaded)
        {
            return;
        }

        await _loadGate.WaitAsync(cancellationToken);
        try
        {
            if (_isLoaded)
            {
                return;
            }

            _entries = await LoadEntriesAsync(cancellationToken);
            _isLoaded = true;
            _logger.LogInformation("Buzz seed knowledge loaded with {Count} entries.", _entries.Count);
        }
        finally
        {
            _loadGate.Release();
        }
    }

    private async Task<IReadOnlyList<BuzzSeedKnowledgeEntry>> LoadEntriesAsync(CancellationToken cancellationToken)
    {
        if (!_options.EnableSeedKnowledgeBootstrap)
        {
            return [];
        }

        var path = (_options.SeedKnowledgeFilePath ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(path))
        {
            return [];
        }

        var resolvedPath = Path.IsPathRooted(path)
            ? path
            : Path.Combine(AppContext.BaseDirectory, path);
        if (!File.Exists(resolvedPath))
        {
            _logger.LogDebug("Buzz seed knowledge file not found: {Path}", resolvedPath);
            return [];
        }

        try
        {
            await using var stream = File.OpenRead(resolvedPath);
            var payload = await JsonSerializer.DeserializeAsync<SeedPayload>(
                stream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                },
                cancellationToken);

            return payload?.Entries?
                .Where(entry =>
                    !string.IsNullOrWhiteSpace(entry.Subject) &&
                    !string.IsNullOrWhiteSpace(entry.Text))
                .Select(entry => entry with
                {
                    Subject = entry.Subject.Trim(),
                    Title = string.IsNullOrWhiteSpace(entry.Title) ? "Seed entry" : entry.Title.Trim(),
                    Component = string.IsNullOrWhiteSpace(entry.Component) ? null : entry.Component.Trim(),
                    Text = entry.Text.Trim(),
                    Tags = entry.Tags?
                        .Where(tag => !string.IsNullOrWhiteSpace(tag))
                        .Select(tag => tag.Trim())
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList()
                })
                .ToList() ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load Buzz seed knowledge file: {Path}", resolvedPath);
            return [];
        }
    }

    private static int Score(BuzzSeedKnowledgeEntry entry, string component, string query)
    {
        var score = 0;

        if (!string.IsNullOrWhiteSpace(component))
        {
            if (!string.IsNullOrWhiteSpace(entry.Component) &&
                entry.Component.Equals(component, StringComparison.OrdinalIgnoreCase))
            {
                score += 80;
            }
            else if (!string.IsNullOrWhiteSpace(entry.Component))
            {
                score -= 10;
            }
        }

        if (string.IsNullOrWhiteSpace(query))
        {
            return score;
        }

        if (entry.Text.Contains(query, StringComparison.OrdinalIgnoreCase))
        {
            score += 40;
        }

        if (entry.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
        {
            score += 30;
        }

        if (entry.Tags is not null && entry.Tags.Any(tag => tag.Contains(query, StringComparison.OrdinalIgnoreCase)))
        {
            score += 20;
        }

        return score;
    }
}
