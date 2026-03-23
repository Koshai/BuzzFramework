namespace Buzz.Provider.Ollama;

/// <summary>
/// Configuration values used by <c>OllamaBuzzProvider</c>.
/// </summary>
public sealed class OllamaBuzzOptions
{
    /// <summary>
    /// Ollama API base URL (e.g. <c>http://localhost:11434/api/</c>).
    /// </summary>
    public string BaseUrl { get; init; } = "http://localhost:11434/api/";

    /// <summary>
    /// Target local Ollama model name.
    /// </summary>
    public string Model { get; init; } = "llama3.1:8b";
}
