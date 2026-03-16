using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

/// <summary>
/// Provides ranking and memory helpers for option-based inputs.
/// </summary>
public interface IBuzzOptionRanker
{
    /// <summary>
    /// Ranks available options using local history and optional AI context.
    /// </summary>
    /// <param name="options">Available options to rank.</param>
    /// <param name="currentInput">Current user input value.</param>
    /// <param name="label">Field label.</param>
    /// <param name="pagePath">Current page path.</param>
    /// <param name="memorySubject">Optional memory subject key.</param>
    /// <param name="referenceText">Optional contextual text.</param>
    /// <param name="maxResults">Maximum number of ranked options to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ranked option list.</returns>
    Task<IReadOnlyList<BuzzRankedOption>> RankAsync(
        IReadOnlyList<string> options,
        string currentInput,
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        int maxResults,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores a selected option for future ranking improvements.
    /// </summary>
    /// <param name="selectedValue">Selected value to remember.</param>
    /// <param name="label">Field label.</param>
    /// <param name="pagePath">Current page path.</param>
    /// <param name="memorySubject">Optional memory subject key.</param>
    /// <param name="referenceText">Optional contextual text.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RememberSelectionAsync(
        string selectedValue,
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        CancellationToken cancellationToken = default);
}
