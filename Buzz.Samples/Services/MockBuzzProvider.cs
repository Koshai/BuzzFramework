using Buzz.Core;

namespace Buzz.Samples.Services;

public sealed class MockBuzzProvider : IBuzzProvider
{
    public string Name => "mock";

    public Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default)
    {
        var cleanedInstruction = request.Instruction.Trim();
        var cleanedInput = request.UserText.Trim();

        var output = $"{cleanedInstruction}: {cleanedInput}";
        return Task.FromResult(new BuzzResponse(output, Name, "rule-based-demo"));
    }
}
