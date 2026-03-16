namespace Buzz.Blazor.Models;

/// <summary>
/// Represents a recommended boolean selection produced by toggle advisor services.
/// </summary>
/// <param name="HasRecommendation">Whether a recommendation is available.</param>
/// <param name="RecommendedValue">Suggested boolean value.</param>
/// <param name="Reason">Optional rationale for the recommendation.</param>
/// <param name="ConfidencePercent">Recommendation confidence score from 0 to 100.</param>
public sealed record BuzzToggleRecommendation(
    bool HasRecommendation,
    bool RecommendedValue,
    string? Reason,
    int ConfidencePercent);
