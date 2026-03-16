namespace Buzz.Blazor.Services;

/// <summary>
/// Generates and stores text suggestions for free-form input controls.
/// </summary>
public interface IBuzzSuggestionService
{
    /// <summary>
    /// Returns ranked suggestions for the current text input.
    /// </summary>
    /// <param name="currentText">Current user input text.</param>
    /// <param name="label">Field label.</param>
    /// <param name="pagePath">Current page path.</param>
    /// <param name="maxResults">Maximum number of suggestions to return.</param>
    /// <param name="includeAi">Includes AI-generated candidates when enabled.</param>
    /// <param name="memorySubject">Optional memory subject key.</param>
    /// <param name="referenceText">Optional contextual text.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Suggested text values.</returns>
    Task<IReadOnlyList<string>> GetSuggestionsAsync(
        string currentText,
        string label,
        string pagePath,
        int maxResults,
        bool includeAi,
        string? memorySubject = null,
        string? referenceText = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores an entered value for future suggestion retrieval.
    /// </summary>
    /// <param name="value">User-entered value to remember.</param>
    /// <param name="label">Field label.</param>
    /// <param name="pagePath">Current page path.</param>
    /// <param name="memorySubject">Optional memory subject key.</param>
    /// <param name="referenceText">Optional contextual text.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RememberEntryAsync(
        string value,
        string label,
        string pagePath,
        string? memorySubject = null,
        string? referenceText = null,
        CancellationToken cancellationToken = default);
}
