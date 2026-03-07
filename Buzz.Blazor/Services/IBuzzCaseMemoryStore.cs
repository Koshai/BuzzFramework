using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

public interface IBuzzCaseMemoryStore
{
    Task RememberAsync(BuzzCaseMemoryItem item, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BuzzCaseMemoryItem>> SearchAsync(
        string subject,
        string query,
        int maxResults,
        CancellationToken cancellationToken = default);
}
