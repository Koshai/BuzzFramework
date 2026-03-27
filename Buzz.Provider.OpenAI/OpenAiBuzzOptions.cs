namespace Buzz.Provider.OpenAI;

/// <summary>
/// Configuration values used by <c>OpenAiBuzzProvider</c>.
/// </summary>
public sealed class OpenAiBuzzOptions
{
    /// <summary>
    /// OpenAI API key.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
    /// <summary>
    /// Target OpenAI model name.
    /// </summary>
    public string Model { get; init; } = "gpt-4o-mini";
    /// <summary>
    /// Upper bound for completion tokens generated per request.
    /// </summary>
    public int MaxOutputTokens { get; init; } = 220;
    /// <summary>
    /// Sampling temperature for generation.
    /// </summary>
    public double Temperature { get; init; } = 0.2;
}

