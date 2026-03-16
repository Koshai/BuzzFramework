namespace Buzz.Core;

/// <summary>
/// Represents a normalized response returned from an AI provider.
/// </summary>
/// <param name="OutputText">Generated output text.</param>
/// <param name="ProviderName">Provider identifier that produced the response.</param>
/// <param name="Model">Optional model name used by the provider.</param>
public sealed record BuzzResponse(
    string OutputText,
    string ProviderName,
    string? Model = null);
