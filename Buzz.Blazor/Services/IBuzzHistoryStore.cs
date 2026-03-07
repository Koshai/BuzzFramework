using Buzz.Blazor.Models;

namespace Buzz.Blazor.Services;

public interface IBuzzHistoryStore
{
    Task<IReadOnlyList<BuzzHistoryItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task SaveAllAsync(IReadOnlyList<BuzzHistoryItem> items, CancellationToken cancellationToken = default);
}
