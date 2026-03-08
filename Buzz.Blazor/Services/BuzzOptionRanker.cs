using Buzz.Blazor.Models;
using Microsoft.Extensions.Options;

namespace Buzz.Blazor.Services;

internal sealed class BuzzOptionRanker : IBuzzOptionRanker
{
    private readonly IBuzzCaseMemoryStore _caseMemoryStore;
    private readonly BuzzOptions _options;

    public BuzzOptionRanker(IBuzzCaseMemoryStore caseMemoryStore, IOptions<BuzzOptions> options)
    {
        _caseMemoryStore = caseMemoryStore;
        _options = options.Value;
    }

    public async Task<IReadOnlyList<BuzzRankedOption>> RankAsync(
        IReadOnlyList<string> options,
        string currentInput,
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        int maxResults,
        CancellationToken cancellationToken = default)
    {
        if (options.Count == 0 || maxResults <= 0)
        {
            return [];
        }

        var input = currentInput.Trim();
        var subject = ResolveSubject(memorySubject, label, pagePath);
        var sharedMatches = await LoadSharedMatchesAsync(subject, referenceText, input, cancellationToken);

        var ranked = options
            .Where(option => !string.IsNullOrWhiteSpace(option))
            .Select(option => option.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Select(option => new
            {
                Option = option,
                Score = ScoreOption(option, input, sharedMatches),
                Reason = BuildReason(option, input, sharedMatches)
            })
            .OrderByDescending(item => item.Score)
            .ThenBy(item => item.Option, StringComparer.OrdinalIgnoreCase)
            .Take(maxResults)
            .Select(item => new BuzzRankedOption(item.Option, item.Reason))
            .ToList();

        return ranked;
    }

    public async Task RememberSelectionAsync(
        string selectedValue,
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        CancellationToken cancellationToken = default)
    {
        if (!_options.EnableSharedCaseMemory)
        {
            return;
        }

        var cleanedValue = selectedValue.Trim();
        if (string.IsNullOrWhiteSpace(cleanedValue))
        {
            return;
        }

        var subject = ResolveSubject(memorySubject, label, pagePath);
        await _caseMemoryStore.RememberAsync(
            new BuzzCaseMemoryItem(
                cleanedValue,
                subject,
                label.Trim(),
                NormalizePagePath(pagePath),
                string.IsNullOrWhiteSpace(referenceText) ? null : referenceText.Trim(),
                DateTimeOffset.UtcNow,
                1),
            cancellationToken);
    }

    private async Task<Dictionary<string, int>> LoadSharedMatchesAsync(
        string subject,
        string? referenceText,
        string input,
        CancellationToken cancellationToken)
    {
        var sharedScores = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        if (!_options.EnableSharedCaseMemory)
        {
            return sharedScores;
        }

        var query = string.IsNullOrWhiteSpace(referenceText) ? input : referenceText.Trim();
        var matches = await _caseMemoryStore.SearchAsync(subject, query, 200, cancellationToken);
        foreach (var match in matches)
        {
            var score = Math.Min(match.UseCount * 3, 40);
            var ageDays = (DateTimeOffset.UtcNow - match.LastUsedUtc).TotalDays;
            if (ageDays <= 1)
            {
                score += 20;
            }
            else if (ageDays <= 7)
            {
                score += 10;
            }

            if (sharedScores.TryGetValue(match.Text, out var existing))
            {
                sharedScores[match.Text] = Math.Max(existing, score);
            }
            else
            {
                sharedScores[match.Text] = score;
            }
        }

        return sharedScores;
    }

    private static int ScoreOption(string option, string input, IReadOnlyDictionary<string, int> sharedScores)
    {
        var score = 0;
        if (string.IsNullOrWhiteSpace(input))
        {
            score += 5;
        }
        else if (option.StartsWith(input, StringComparison.OrdinalIgnoreCase))
        {
            score += 80;
        }
        else if (option.Contains(input, StringComparison.OrdinalIgnoreCase))
        {
            score += 35;
        }

        if (sharedScores.TryGetValue(option, out var memoryBoost))
        {
            score += memoryBoost;
        }

        return score;
    }

    private static string? BuildReason(string option, string input, IReadOnlyDictionary<string, int> sharedScores)
    {
        if (sharedScores.ContainsKey(option))
        {
            return "Frequent in similar cases";
        }

        if (!string.IsNullOrWhiteSpace(input) &&
            option.StartsWith(input, StringComparison.OrdinalIgnoreCase))
        {
            return "Best typed match";
        }

        if (!string.IsNullOrWhiteSpace(input) &&
            option.Contains(input, StringComparison.OrdinalIgnoreCase))
        {
            return "Related typed match";
        }

        return null;
    }

    private static string ResolveSubject(string? memorySubject, string label, string pagePath)
    {
        if (!string.IsNullOrWhiteSpace(memorySubject))
        {
            return memorySubject.Trim();
        }

        return $"{NormalizePagePath(pagePath)}::{label.Trim()}";
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
}
