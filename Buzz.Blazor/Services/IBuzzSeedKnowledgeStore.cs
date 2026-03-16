using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

/// <summary>
/// Stores baseline domain knowledge used for AI cold-start context enrichment.
/// </summary>
public interface IBuzzSeedKnowledgeStore
{
    /// <summary>
    /// Preloads seed knowledge data into memory.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task WarmupAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds relevant seed entries by subject, component, and query text.
    /// </summary>
    /// <param name="subject">Knowledge subject bucket.</param>
    /// <param name="component">Optional component key.</param>
    /// <param name="query">Current query text used for ranking.</param>
    /// <param name="maxResults">Maximum entries to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ranked seed knowledge entries.</returns>
    Task<IReadOnlyList<BuzzSeedKnowledgeEntry>> SearchAsync(
        string subject,
        string? component,
        string query,
        int maxResults,
        CancellationToken cancellationToken = default);
}
