namespace Buzz.Blazor.Models;

/// <summary>
/// Represents an option item used by menu and dropdown components.
/// </summary>
/// <param name="Text">Displayed menu label.</param>
/// <param name="Value">Underlying value emitted on selection.</param>
/// <param name="Disabled">When <see langword="true"/>, prevents selection.</param>
public sealed record BuzzMenuItem(
    string Text,
    string Value,
    bool Disabled = false);
