using System.Text;
using Buzz.Blazor.Models;
using Microsoft.Extensions.Options;

namespace Buzz.Blazor.Services;

internal sealed class BuzzAiContextComposer : IBuzzAiContextComposer
{
    private readonly BuzzOptions _options;
    private readonly IBuzzSeedKnowledgeStore _seedKnowledgeStore;
    private readonly IBuzzCaseMemoryStore _caseMemoryStore;

    public BuzzAiContextComposer(
        IOptions<BuzzOptions> options,
        IBuzzSeedKnowledgeStore seedKnowledgeStore,
        IBuzzCaseMemoryStore caseMemoryStore)
    {
        _options = options.Value;
        _seedKnowledgeStore = seedKnowledgeStore;
        _caseMemoryStore = caseMemoryStore;
    }

    public async Task<string> ComposeAsync(
        string component,
        string subject,
        string sourceText,
        string? userText,
        int maxCharacters,
        CancellationToken cancellationToken = default)
    {
        var safeMax = Math.Max(400, maxCharacters);
        var builder = new StringBuilder();
        var normalizedComponent = string.IsNullOrWhiteSpace(component) ? "unknown" : component.Trim();
        var normalizedSubject = string.IsNullOrWhiteSpace(subject) ? "general" : subject.Trim();
        var query = string.IsNullOrWhiteSpace(userText) ? sourceText : userText;

        builder.AppendLine($"Component: {normalizedComponent}");
        builder.AppendLine($"Subject: {normalizedSubject}");

        if (!string.IsNullOrWhiteSpace(userText))
        {
            builder.AppendLine("UserContext (highest precedence):");
            builder.AppendLine(Truncate(userText.Trim(), safeMax / 3));
        }

        if (!string.IsNullOrWhiteSpace(sourceText))
        {
            builder.AppendLine("DeveloperContext:");
            builder.AppendLine(Truncate(sourceText.Trim(), safeMax / 2));
        }

        if (_options.EnableAiContextEnrichment)
        {
            var seedEntries = await _seedKnowledgeStore.SearchAsync(
                normalizedSubject,
                normalizedComponent,
                query ?? string.Empty,
                Math.Max(1, _options.SeedKnowledgeMaxMatchesPerRequest),
                cancellationToken);
            AppendSeedEntries(builder, seedEntries);

            var memoryEntries = await _caseMemoryStore.SearchAsync(
                normalizedSubject,
                query ?? string.Empty,
                Math.Max(1, _options.UserMemoryMaxMatchesPerRequest),
                cancellationToken);
            AppendMemoryEntries(builder, memoryEntries);
        }

        return Truncate(builder.ToString(), safeMax);
    }

    private static void AppendSeedEntries(StringBuilder builder, IReadOnlyList<BuzzSeedKnowledgeEntry> entries)
    {
        if (entries.Count == 0)
        {
            return;
        }

        builder.AppendLine("SeedKnowledge (baseline defaults):");
        foreach (var entry in entries)
        {
            builder.Append("- ");
            builder.Append(entry.Title);
            builder.Append(": ");
            builder.AppendLine(entry.Text);
        }
    }

    private static void AppendMemoryEntries(StringBuilder builder, IReadOnlyList<BuzzCaseMemoryItem> entries)
    {
        if (entries.Count == 0)
        {
            return;
        }

        builder.AppendLine("SharedUserMemory (overrides baseline when conflicting):");
        foreach (var entry in entries)
        {
            builder.Append("- ");
            builder.Append(entry.Label);
            builder.Append(": ");
            builder.AppendLine(entry.Text);
        }
    }

    private static string Truncate(string text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text) || maxLength <= 0 || text.Length <= maxLength)
        {
            return text;
        }

        return text[..maxLength];
    }
}
