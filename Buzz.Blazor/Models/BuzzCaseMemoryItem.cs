namespace Buzz.Blazor.Models;

public sealed record BuzzCaseMemoryItem(
    string Text,
    string Subject,
    string Label,
    string PagePath,
    string? ReferenceText,
    DateTimeOffset LastUsedUtc,
    int UseCount);
