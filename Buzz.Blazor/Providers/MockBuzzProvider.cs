using Buzz.Core;

namespace Buzz.Blazor.Providers;

/// <summary>
/// Mock Buzz provider that echoes input for testing and fallback when no real AI is configured.
/// </summary>
internal sealed class MockBuzzProvider : IBuzzProvider
{
    public string Name => "mock";

    public Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default)
    {
        var instruction = (request.Instruction ?? "").Trim();
        var userText = (request.UserText ?? "").Trim();
        var output = string.IsNullOrWhiteSpace(instruction)
            ? $"Received: {userText}"
            : $"{instruction}: {userText}";
        if (!string.IsNullOrWhiteSpace(request.Context))
        {
            output += $"\n[Context: {request.Context.Length} chars]";
        }
        return Task.FromResult(new BuzzResponse(output, Name, "mock"));
    }
}
