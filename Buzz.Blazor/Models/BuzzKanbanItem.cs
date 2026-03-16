namespace Buzz.Blazor.Models;

/// <summary>
/// Defines a card item rendered in <c>BuzzKanbanBoard</c>.
/// </summary>
/// <param name="Id">Unique card identifier.</param>
/// <param name="Title">Card title shown to users.</param>
/// <param name="Description">Supporting card details.</param>
/// <param name="Column">Current column key that owns this card.</param>
/// <param name="Severity">Visual severity hint (for example: info, warning, error, success).</param>
public sealed record BuzzKanbanItem(
    string Id,
    string Title,
    string Description,
    string Column,
    string Severity = "info");
