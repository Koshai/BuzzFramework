namespace Buzz.Blazor.Services;

/// <summary>
/// Composes AI prompt context from seed knowledge, shared memory, and component-local state.
/// </summary>
public interface IBuzzAiContextComposer
{
    /// <summary>
    /// Builds enriched context text for AI requests.
    /// </summary>
    /// <param name="component">Component key generating the request.</param>
    /// <param name="subject">Domain subject bucket for seed and memory lookup.</param>
    /// <param name="sourceText">Developer-provided source context.</param>
    /// <param name="userText">Current user/session text that should take precedence.</param>
    /// <param name="maxCharacters">Maximum length of the returned context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Composed context string for AI prompt usage.</returns>
    Task<string> ComposeAsync(
        string component,
        string subject,
        string sourceText,
        string? userText,
        int maxCharacters,
        CancellationToken cancellationToken = default);
}
