namespace Buzz.Blazor.Models;

/// <summary>
/// Holds a start and end date (ISO-style string values) for range-based controls.
/// </summary>
/// <param name="Start">Range start date value.</param>
/// <param name="End">Range end date value.</param>
public sealed record BuzzDateRange(
    string Start,
    string End);
