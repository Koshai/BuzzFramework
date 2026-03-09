using Buzz.Blazor.Models;
using Microsoft.Extensions.Options;

namespace Buzz.Blazor.Services;

internal sealed class BuzzToggleAdvisor : IBuzzToggleAdvisor
{
    private const string CheckedToken = "__checked__";
    private const string UncheckedToken = "__unchecked__";

    private readonly IBuzzCaseMemoryStore _caseMemoryStore;
    private readonly BuzzOptions _options;

    public BuzzToggleAdvisor(IBuzzCaseMemoryStore caseMemoryStore, IOptions<BuzzOptions> options)
    {
        _caseMemoryStore = caseMemoryStore;
        _options = options.Value;
    }

    public async Task<BuzzToggleRecommendation> RecommendAsync(
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        CancellationToken cancellationToken = default)
    {
        if (!_options.EnableSharedCaseMemory)
        {
            return new BuzzToggleRecommendation(false, false, null, 0);
        }

        var subject = ResolveSubject(memorySubject, label, pagePath);
        var query = string.IsNullOrWhiteSpace(referenceText) ? label : referenceText.Trim();
        var matches = await _caseMemoryStore.SearchAsync(subject, query, 300, cancellationToken);

        var checkedScore = Score(matches, CheckedToken);
        var uncheckedScore = Score(matches, UncheckedToken);
        var total = checkedScore + uncheckedScore;
        if (total == 0)
        {
            return new BuzzToggleRecommendation(false, false, null, 0);
        }

        var recommendChecked = checkedScore >= uncheckedScore;
        var max = Math.Max(checkedScore, uncheckedScore);
        var confidence = (int)Math.Clamp(Math.Round((double)max / total * 100), 0, 100);
        var reason = recommendChecked
            ? "Enabled more often in similar cases"
            : "Disabled more often in similar cases";

        return new BuzzToggleRecommendation(true, recommendChecked, reason, confidence);
    }

    public async Task RememberSelectionAsync(
        bool value,
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

        var token = value ? CheckedToken : UncheckedToken;
        var subject = ResolveSubject(memorySubject, label, pagePath);
        await _caseMemoryStore.RememberAsync(
            new BuzzCaseMemoryItem(
                token,
                subject,
                label.Trim(),
                NormalizePagePath(pagePath),
                string.IsNullOrWhiteSpace(referenceText) ? null : referenceText.Trim(),
                DateTimeOffset.UtcNow,
                1),
            cancellationToken);
    }

    private static int Score(IReadOnlyList<BuzzCaseMemoryItem> items, string token)
    {
        var score = 0;
        var now = DateTimeOffset.UtcNow;
        foreach (var item in items)
        {
            if (!item.Text.Equals(token, StringComparison.Ordinal))
            {
                continue;
            }

            score += Math.Min(item.UseCount * 4, 40);
            var ageDays = (now - item.LastUsedUtc).TotalDays;
            if (ageDays <= 1)
            {
                score += 20;
            }
            else if (ageDays <= 7)
            {
                score += 10;
            }
        }

        return score;
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
