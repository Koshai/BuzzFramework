namespace Buzz.Blazor.Models;

/// <summary>
/// Payload emitted when a kanban card is moved between columns.
/// </summary>
/// <param name="ItemId">Moved card identifier.</param>
/// <param name="FromColumn">Source column key.</param>
/// <param name="ToColumn">Destination column key.</param>
public sealed record BuzzKanbanMoveEvent(
    string ItemId,
    string FromColumn,
    string ToColumn);
