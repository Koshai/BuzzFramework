namespace Buzz.Core;

/// <summary>
/// Unified client abstraction used by components to request AI output.
/// </summary>
public interface IBuzzClient
{
    /// <summary>
    /// Generates output by routing the request through configured providers and failover order.
    /// </summary>
    /// <param name="request">Normalized generation request payload.</param>
    /// <param name="cancellationToken">Cancellation signal for the operation.</param>
    /// <returns>A normalized generation response.</returns>
    Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default);
}
