namespace Buzz.Blazor.Models;

/// <summary>
/// Defines a navigation entry rendered by Buzz navigation components.
/// </summary>
/// <param name="Text">Visible link text.</param>
/// <param name="Href">Navigation target URL.</param>
/// <param name="MatchAll">When <see langword="true"/>, uses exact route matching.</param>
public sealed record BuzzNavLink(
    string Text,
    string Href,
    bool MatchAll = false);
