using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

public interface IBuzzToggleAdvisor
{
    Task<BuzzToggleRecommendation> RecommendAsync(
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        CancellationToken cancellationToken = default);

    Task RememberSelectionAsync(
        bool value,
        string label,
        string pagePath,
        string? memorySubject,
        string? referenceText,
        CancellationToken cancellationToken = default);
}
