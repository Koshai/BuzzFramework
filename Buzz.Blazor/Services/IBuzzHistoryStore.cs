using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

/// <summary>
/// Persistence abstraction for user-entry history used by suggestion services.
/// </summary>
public interface IBuzzHistoryStore
{
    /// <summary>
    /// Reads the full history collection.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>All stored history entries.</returns>
    Task<IReadOnlyList<BuzzHistoryItem>> GetAllAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Persists the full history collection.
    /// </summary>
    /// <param name="items">History items to store.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SaveAllAsync(IReadOnlyList<BuzzHistoryItem> items, CancellationToken cancellationToken = default);
}
