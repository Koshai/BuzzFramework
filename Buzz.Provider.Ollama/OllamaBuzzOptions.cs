namespace Buzz.Provider.Ollama;

/// <summary>
/// Configuration values used by <c>OllamaBuzzProvider</c>.
/// </summary>
public sealed class OllamaBuzzOptions
{
    /// <summary>
    /// Target local Ollama model name.
    /// </summary>
    public string Model { get; init; } = "llama3.1:8b";
}
