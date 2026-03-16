using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

/// <summary>
/// Produces recommendations and memory for boolean/toggle style inputs.
/// </summary>
public interface IBuzzToggleAdvisor
{
    /// <summary>
    /// Returns a recommended toggle value based on history and optional context.
    /// </summary>
    /// <param name="label">Field label.</param>
    /// <param name="pagePath">Current page path.</param>
    /// <param name="memorySubject">Optional memory subject key.</param>
    /// <param name="referenceText">Optional contextual text.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Recommendation payload including reason and confidence.</returns>
    Task<BuzzToggleRecommendation> RecommendAsync(
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores the selected toggle value for future recommendation quality.
    /// </summary>
    /// <param name="value">Selected toggle value.</param>
    /// <param name="label">Field label.</param>
    /// <param name="pagePath">Current page path.</param>
    /// <param name="memorySubject">Optional memory subject key.</param>
    /// <param name="referenceText">Optional contextual text.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RememberSelectionAsync(
        bool value,
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        CancellationToken cancellationToken = default);
}
