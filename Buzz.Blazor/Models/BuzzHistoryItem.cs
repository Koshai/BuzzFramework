namespace Buzz.Blazor.Models;

/// <summary>
/// Represents one historical text entry used by suggestion services.
/// </summary>
/// <param name="Text">Captured user text value.</param>
/// <param name="Label">Field label where this value was entered.</param>
/// <param name="PagePath">Route where the value was captured.</param>
/// <param name="LastUsedUtc">Most recent usage timestamp in UTC.</param>
/// <param name="UseCount">How often the value has been reused.</param>
public sealed record BuzzHistoryItem(
    string Text,
    string Label,
    string PagePath,
    DateTimeOffset LastUsedUtc,
    int UseCount);
