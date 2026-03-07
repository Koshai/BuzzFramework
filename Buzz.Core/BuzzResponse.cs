namespace Buzz.Core;

public sealed record BuzzResponse(
    string OutputText,
    string ProviderName,
    string? Model = null);
