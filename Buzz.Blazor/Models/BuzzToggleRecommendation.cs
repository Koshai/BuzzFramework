namespace Buzz.Blazor.Models;

public sealed record BuzzToggleRecommendation(
    bool HasRecommendation,
    bool RecommendedValue,
    string? Reason,
    int ConfidencePercent);
