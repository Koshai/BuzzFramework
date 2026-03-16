using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

/// <summary>
/// Storage contract for shared case-memory entries used by AI suggestion features.
/// </summary>
public interface IBuzzCaseMemoryStore
{
    /// <summary>
    /// Persists or updates one memory entry.
    /// </summary>
    /// <param name="item">Memory item to store.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RememberAsync(BuzzCaseMemoryItem item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches memory entries by subject and query.
    /// </summary>
    /// <param name="subject">Subject bucket to search in.</param>
    /// <param name="query">Free-text query.</param>
    /// <param name="maxResults">Maximum number of results to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Matching memory entries ordered by implementation-defined relevance.</returns>
    Task<IReadOnlyList<BuzzCaseMemoryItem>> SearchAsync(
        string subject,
        string query,
        int maxResults,
        CancellationToken cancellationToken = default);
}
