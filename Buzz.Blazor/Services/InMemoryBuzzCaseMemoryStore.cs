using System.Collections.Concurrent;
using Buzz.Blazor.Models;
using Microsoft.Extensions.Options;

namespace Buzz.Blazor.Services;

internal sealed class InMemoryBuzzCaseMemoryStore : IBuzzCaseMemoryStore
{
    private readonly ConcurrentDictionary<string, List<BuzzCaseMemoryItem>> _entriesBySubject =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly BuzzOptions _options;

    public InMemoryBuzzCaseMemoryStore(IOptions<BuzzOptions> options)
    {
        _options = options.Value;
    }

    public Task RememberAsync(BuzzCaseMemoryItem item, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(item.Subject) || string.IsNullOrWhiteSpace(item.Text))
        {
            return Task.CompletedTask;
        }

        var subject = item.Subject.Trim();
        var list = _entriesBySubject.GetOrAdd(subject, _ => []);
        lock (list)
        {
            var existingIndex = list.FindIndex(entry =>
                entry.Text.Equals(item.Text, StringComparison.OrdinalIgnoreCase) &&
                entry.Label.Equals(item.Label, StringComparison.OrdinalIgnoreCase) &&
                entry.PagePath.Equals(item.PagePath, StringComparison.OrdinalIgnoreCase));

            if (existingIndex >= 0)
            {
                var existing = list[existingIndex];
                list[existingIndex] = existing with
                {
                    LastUsedUtc = DateTimeOffset.UtcNow,
                    UseCount = existing.UseCount + 1,
                    ReferenceText = item.ReferenceText
                };
            }
            else
            {
                list.Add(item);
            }

            if (list.Count > _options.SharedCaseMemoryMaxEntriesPerSubject)
            {
                var trimmed = list
                    .OrderByDescending(entry => entry.LastUsedUtc)
                    .Take(_options.SharedCaseMemoryMaxEntriesPerSubject)
                    .ToList();
                list.Clear();
                list.AddRange(trimmed);
            }
        }

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<BuzzCaseMemoryItem>> SearchAsync(
        string subject,
        string query,
        int maxResults,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(subject) || maxResults <= 0)
        {
            return Task.FromResult<IReadOnlyList<BuzzCaseMemoryItem>>([]);
        }

        if (!_entriesBySubject.TryGetValue(subject.Trim(), out var list))
        {
            return Task.FromResult<IReadOnlyList<BuzzCaseMemoryItem>>([]);
        }

        var cleanedQuery = query.Trim();
        var now = DateTimeOffset.UtcNow;
        List<BuzzCaseMemoryItem> result;
        lock (list)
        {
            result = list
                .Where(item => !string.IsNullOrWhiteSpace(item.Text))
                .OrderByDescending(item => Score(item, cleanedQuery, now))
                .ThenBy(item => item.Text, StringComparer.OrdinalIgnoreCase)
                .Take(maxResults)
                .ToList();
        }

        return Task.FromResult<IReadOnlyList<BuzzCaseMemoryItem>>(result);
    }

    private static int Score(BuzzCaseMemoryItem item, string query, DateTimeOffset now)
    {
        var score = 0;
        if (!string.IsNullOrWhiteSpace(query))
        {
            if (item.Text.StartsWith(query, StringComparison.OrdinalIgnoreCase))
            {
                score += 90;
            }
            else if (item.Text.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                score += 30;
            }

            if (!string.IsNullOrWhiteSpace(item.ReferenceText) &&
                item.ReferenceText.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                score += 40;
            }
        }

        score += Math.Min(item.UseCount * 2, 30);
        var ageDays = (now - item.LastUsedUtc).TotalDays;
        if (ageDays <= 1)
        {
            score += 20;
        }
        else if (ageDays <= 7)
        {
            score += 10;
        }

        return score;
    }
}
