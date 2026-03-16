namespace Buzz.Core;

/// <summary>
/// Provider contract implemented by concrete AI backends (OpenAI, Ollama, mock, etc.).
/// </summary>
public interface IBuzzProvider
{
    /// <summary>
    /// Gets the provider name used for registration and selection.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Generates an AI response for the provided request.
    /// </summary>
    /// <param name="request">Normalized generation request payload.</param>
    /// <param name="cancellationToken">Cancellation signal for the operation.</param>
    /// <returns>A normalized generation response from this provider.</returns>
    Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default);
}
