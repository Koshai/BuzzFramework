namespace Buzz.Blazor.Models;

/// <summary>
/// Stores a reusable memory entry used by AI-assisted suggestions.
/// </summary>
/// <param name="Text">Original user-entered text.</param>
/// <param name="Subject">Logical subject bucket for grouping memory entries.</param>
/// <param name="Label">UI label associated with the source field.</param>
/// <param name="PagePath">Application route where the entry was captured.</param>
/// <param name="ReferenceText">Optional source context used during capture.</param>
/// <param name="LastUsedUtc">Timestamp of the most recent usage in UTC.</param>
/// <param name="UseCount">How many times this memory item was reused.</param>
public sealed record BuzzCaseMemoryItem(
    string Text,
    string Subject,
    string Label,
    string PagePath,
    string? ReferenceText,
    DateTimeOffset LastUsedUtc,
    int UseCount);
