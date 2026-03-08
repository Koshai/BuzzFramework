using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

public interface IBuzzOptionRanker
{
    Task<IReadOnlyList<BuzzRankedOption>> RankAsync(
        IReadOnlyList<string> options,
        string currentInput,
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        int maxResults,
        CancellationToken cancellationToken = default);

    Task RememberSelectionAsync(
        string selectedValue,
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        CancellationToken cancellationToken = default);
}
