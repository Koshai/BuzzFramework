namespace Buzz.Core;

public interface IBuzzClient
{
    Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default);
}
