namespace Buzz.Provider.OpenAI;

/// <summary>
/// Configuration values used by <c>OpenAiBuzzProvider</c>.
/// </summary>
public sealed class OpenAiBuzzOptions
{
    /// <summary>
    /// OpenAI API key.
    /// </summary>
    public string ApiKey { get; init; } = string.Empty;
    /// <summary>
    /// Target OpenAI model name.
    /// </summary>
    public string Model { get; init; } = "gpt-4o-mini";
}
