namespace Buzz.Blazor.Models;

/// <summary>
/// Represents an item shown in <c>BuzzCommandPalette</c>.
/// </summary>
/// <param name="Title">Short command name users can quickly scan.</param>
/// <param name="Description">Additional context for the command.</param>
/// <param name="Value">Stable value emitted when the command is selected.</param>
public sealed record BuzzCommandItem(
    string Title,
    string Description,
    string Value);
