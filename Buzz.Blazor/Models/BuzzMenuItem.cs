namespace Buzz.Blazor.Models;

public sealed record BuzzMenuItem(
    string Text,
    string Value,
    bool Disabled = false);
