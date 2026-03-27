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
    /// Target Ollama model name. Use any model installed locally (e.g. llama3.2:latest, mistral, phi).
    /// Configure via Buzz:Ollama:Model in appsettings or the configure delegate.
    /// </summary>
    public string Model { get; init; } = "llama3.2:latest";
}
