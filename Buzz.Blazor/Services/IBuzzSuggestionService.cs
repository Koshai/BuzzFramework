namespace Buzz.Blazor.Services;

public interface IBuzzSuggestionService
{
    Task<IReadOnlyList<string>> GetSuggestionsAsync(
        string currentText,
        string label,
        string pagePath,
        int maxResults,
        bool includeAi,
        string? memorySubject = null,
        string? referenceText = null,
        CancellationToken cancellationToken = default);

    Task RememberEntryAsync(
        string value,
        string label,
        string pagePath,
        string? memorySubject = null,
        string? referenceText = null,
        CancellationToken cancellationToken = default);
}
