namespace Buzz.Blazor.Models;

public sealed record BuzzNavLink(
    string Text,
    string Href,
    bool MatchAll = false);
