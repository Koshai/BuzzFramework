namespace Buzz.Blazor.Models;

public sealed record BuzzHistoryItem(
    string Text,
    string Label,
    string PagePath,
    DateTimeOffset LastUsedUtc,
    int UseCount);
