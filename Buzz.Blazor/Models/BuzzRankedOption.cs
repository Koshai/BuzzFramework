namespace Buzz.Blazor.Models;

/// <summary>
/// Represents an option candidate ranked for recommendation.
/// </summary>
/// <param name="Value">Option value.</param>
/// <param name="Reason">Optional explanation for why the option was ranked.</param>
public sealed record BuzzRankedOption(
    string Value,
    string? Reason = null);
