namespace Buzz.Core;

public interface IBuzzProvider
{
    string Name { get; }

    Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default);
}
