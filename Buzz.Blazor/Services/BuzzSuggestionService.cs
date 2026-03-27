using Buzz.Blazor.Models;
using Buzz.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Buzz.Blazor.Services;

internal sealed class BuzzSuggestionService : IBuzzSuggestionService
{
    private readonly IBuzzHistoryStore _historyStore;
    private readonly IBuzzCaseMemoryStore _caseMemoryStore;
    private readonly IBuzzClient _buzzClient;
    private readonly BuzzOptions _options;
    private readonly ILogger<BuzzSuggestionService> _logger;
    private readonly object _syncLock = new();
    private readonly Dictionary<string, DateTimeOffset> _lastAiCallByContext = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, CachedAiSuggestions> _aiCache = new(StringComparer.OrdinalIgnoreCase);

    public BuzzSuggestionService(
        IBuzzHistoryStore historyStore,
        IBuzzCaseMemoryStore caseMemoryStore,
        IBuzzClient buzzClient,
        IOptions<BuzzOptions> options,
        ILogger<BuzzSuggestionService> logger)
    {
        _historyStore = historyStore;
        _caseMemoryStore = caseMemoryStore;
        _buzzClient = buzzClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<IReadOnlyList<string>> GetSuggestionsAsync(
        string currentText,
        string label,
        string pagePath,
        int maxResults,
        bool includeAi,
        string? memorySubject = null,
        string? referenceText = null,
        CancellationToken cancellationToken = default)
    {
        var cleanedInput = currentText.Trim();
        if (string.IsNullOrWhiteSpace(cleanedInput) || maxResults <= 0)
        {
            return [];
        }

        var entries = await _historyStore.GetAllAsync(cancellationToken);
        var local = entries
            .Where(item => !string.IsNullOrWhiteSpace(item.Text))
            .Where(item => !item.Text.Equals(cleanedInput, StringComparison.OrdinalIgnoreCase))
            .Select(item => new
            {
                item.Text,
                Score = Score(item, cleanedInput, label, pagePath)
            })
            .Where(item => item.Score > 0)
            .OrderByDescending(item => item.Score)
            .ThenBy(item => item.Text, StringComparer.OrdinalIgnoreCase)
            .Select(item => item.Text)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Take(maxResults)
            .ToList();

        var finalResults = local;
        if (_options.EnableSharedCaseMemory && !string.IsNullOrWhiteSpace(memorySubject))
        {
            var sharedQuery = string.IsNullOrWhiteSpace(referenceText) ? cleanedInput : referenceText;
            var sharedMatches = await _caseMemoryStore.SearchAsync(
                memorySubject,
                sharedQuery,
                maxResults,
                cancellationToken);
            finalResults = finalResults
                .Concat(sharedMatches.Select(item => item.Text))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Take(maxResults)
                .ToList();
        }

        if (includeAi)
        {
            var aiSuggestions = await GetAiSuggestionsAsync(
                cleanedInput,
                label,
                pagePath,
                maxResults,
                local.Count,
                cancellationToken);

            finalResults = local
                .Concat(aiSuggestions)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Take(maxResults)
                .ToList();
        }

        _logger.LogDebug(
            "Buzz suggestions requested. Input='{Input}', Label='{Label}', Page='{Page}', IncludeAi={IncludeAi}, Results={Count}",
            cleanedInput,
            label,
            pagePath,
            includeAi,
            finalResults.Count);

        return finalResults;
    }

    public async Task RememberEntryAsync(
        string value,
        string label,
        string pagePath,
        string? memorySubject = null,
        string? referenceText = null,
        CancellationToken cancellationToken = default)
    {
        var cleanedValue = value.Trim();
        if (string.IsNullOrWhiteSpace(cleanedValue))
        {
            return;
        }

        var normalizedLabel = label.Trim();
        var normalizedPagePath = NormalizePagePath(pagePath);
        var entries = (await _historyStore.GetAllAsync(cancellationToken)).ToList();
        var existingIndex = entries.FindIndex(item =>
            item.Text.Equals(cleanedValue, StringComparison.OrdinalIgnoreCase) &&
            item.Label.Equals(normalizedLabel, StringComparison.OrdinalIgnoreCase) &&
            item.PagePath.Equals(normalizedPagePath, StringComparison.OrdinalIgnoreCase));

        if (existingIndex >= 0)
        {
            var existing = entries[existingIndex];
            entries[existingIndex] = existing with
            {
                LastUsedUtc = DateTimeOffset.UtcNow,
                UseCount = existing.UseCount + 1
            };
        }
        else
        {
            entries.Add(new BuzzHistoryItem(
                cleanedValue,
                normalizedLabel,
                normalizedPagePath,
                DateTimeOffset.UtcNow,
                1));
        }

        if (entries.Count > _options.MaxHistoryEntries)
        {
            entries = entries
                .OrderByDescending(item => item.LastUsedUtc)
                .Take(_options.MaxHistoryEntries)
                .ToList();
        }

        await _historyStore.SaveAllAsync(entries, cancellationToken);
        if (_options.EnableSharedCaseMemory && !string.IsNullOrWhiteSpace(memorySubject))
        {
            await _caseMemoryStore.RememberAsync(
                new BuzzCaseMemoryItem(
                    cleanedValue,
                    memorySubject.Trim(),
                    normalizedLabel,
                    normalizedPagePath,
                    string.IsNullOrWhiteSpace(referenceText) ? null : referenceText.Trim(),
                    DateTimeOffset.UtcNow,
                    1),
                cancellationToken);
        }

        _logger.LogDebug(
            "Buzz history saved. Text='{Text}', Label='{Label}', Page='{Page}', Subject='{Subject}', TotalEntries={Count}",
            cleanedValue,
            normalizedLabel,
            normalizedPagePath,
            memorySubject ?? string.Empty,
            entries.Count);
    }

    private async Task<IReadOnlyList<string>> GetAiSuggestionsAsync(
        string currentText,
        string label,
        string pagePath,
        int maxResults,
        int localResultCount,
        CancellationToken cancellationToken)
    {
        if (!_options.EnableAiSuggestions)
        {
            return [];
        }

        if (currentText.Length < _options.AiMinInputLength)
        {
            return [];
        }

        if (localResultCount >= _options.AiMaxLocalResultsBeforeSkip)
        {
            return [];
        }

        var contextKey = BuildContextKey(label, pagePath);
        var cacheKey = BuildCacheKey(currentText, label, pagePath, maxResults);
        var now = DateTimeOffset.UtcNow;

        lock (_syncLock)
        {
            if (_aiCache.TryGetValue(cacheKey, out var cached) &&
                cached.ExpiresAtUtc > now)
            {
                return cached.Suggestions;
            }

            if (_lastAiCallByContext.TryGetValue(contextKey, out var lastCall))
            {
                var elapsed = now - lastCall;
                if (elapsed.TotalSeconds < _options.AiCooldownSeconds)
                {
                    return [];
                }
            }

            _lastAiCallByContext[contextKey] = now;
        }

        try
        {
            var boundedInput = Truncate(currentText, Math.Max(50, _options.AiMaxUserInputCharacters));
            var prompt = BuildAiSuggestionPrompt(boundedInput, label, pagePath, maxResults);
            var response = await _buzzClient.GenerateAsync(
                new BuzzRequest(boundedInput, prompt, Truncate($"Field label: {label}. Page path: {NormalizePagePath(pagePath)}.", Math.Max(100, _options.AiMaxPromptCharacters / 3))),
                cancellationToken);

            var parsed = ParseAiSuggestions(response.OutputText, maxResults);
            if (parsed.Count == 0)
            {
                return [];
            }

            lock (_syncLock)
            {
                _aiCache[cacheKey] = new CachedAiSuggestions(
                    parsed,
                    DateTimeOffset.UtcNow.AddSeconds(_options.AiCacheTtlSeconds));
            }

            _logger.LogDebug(
                "AI suggestions generated via provider '{Provider}'. Input='{Input}', Count={Count}",
                response.ProviderName,
                currentText,
                parsed.Count);
            return parsed;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(
                ex,
                "AI suggestion generation failed for input '{Input}' on page '{Page}'",
                currentText,
                pagePath);
            return [];
        }
    }

    private static int Score(BuzzHistoryItem item, string currentText, string label, string pagePath)
    {
        var score = 0;
        if (item.Text.StartsWith(currentText, StringComparison.OrdinalIgnoreCase))
        {
            score += 100;
        }
        else if (item.Text.Contains(currentText, StringComparison.OrdinalIgnoreCase))
        {
            score += 35;
        }

        if (item.Label.Equals(label, StringComparison.OrdinalIgnoreCase))
        {
            score += 40;
        }
        else if (!string.IsNullOrWhiteSpace(label) &&
                 item.Label.Contains(label, StringComparison.OrdinalIgnoreCase))
        {
            score += 20;
        }

        var normalizedPagePath = NormalizePagePath(pagePath);
        if (item.PagePath.Equals(normalizedPagePath, StringComparison.OrdinalIgnoreCase))
        {
            score += 30;
        }

        score += Math.Min(item.UseCount * 2, 30);

        var ageDays = (DateTimeOffset.UtcNow - item.LastUsedUtc).TotalDays;
        if (ageDays <= 1)
        {
            score += 20;
        }
        else if (ageDays <= 7)
        {
            score += 10;
        }
        else if (ageDays <= 30)
        {
            score += 5;
        }

        return score;
    }

    private static string NormalizePagePath(string pagePath)
    {
        if (string.IsNullOrWhiteSpace(pagePath))
        {
            return "/";
        }

        var trimmed = pagePath.Trim();
        return trimmed.StartsWith('/') ? trimmed : $"/{trimmed}";
    }

    private static string BuildContextKey(string label, string pagePath)
    {
        return $"{label.Trim().ToLowerInvariant()}|{NormalizePagePath(pagePath).ToLowerInvariant()}";
    }

    private static string BuildCacheKey(string currentText, string label, string pagePath, int maxResults)
    {
        return $"{currentText.Trim().ToLowerInvariant()}|{BuildContextKey(label, pagePath)}|{maxResults}";
    }

    private static string BuildAiSuggestionPrompt(string currentText, string label, string pagePath, int maxResults)
    {
        return
            $"Suggest up to {maxResults} likely user completions for this textbox.\n" +
            $"Label: {label}\n" +
            $"Page: {NormalizePagePath(pagePath)}\n" +
            $"Current text: {currentText}\n" +
            "Return only suggestions, one per line, no numbering.";
    }

    private static string Truncate(string text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text) || maxLength <= 0 || text.Length <= maxLength)
        {
            return text;
        }

        return text[..maxLength];
    }

    private static List<string> ParseAiSuggestions(string output, int maxResults)
    {
        return output
            .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(line => line.TrimStart('-', '*', ' ', '\t'))
            .Select(line =>
            {
                var dotIndex = line.IndexOf('.');
                if (dotIndex > 0 && int.TryParse(line[..dotIndex], out _))
                {
                    return line[(dotIndex + 1)..].Trim();
                }

                return line;
            })
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Take(maxResults)
            .ToList();
    }

    private sealed record CachedAiSuggestions(
        IReadOnlyList<string> Suggestions,
        DateTimeOffset ExpiresAtUtc);
}

