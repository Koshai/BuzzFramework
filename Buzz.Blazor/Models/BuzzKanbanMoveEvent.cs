namespace Buzz.Blazor.Models;

public sealed record BuzzKanbanMoveEvent(
    string ItemId,
    string FromColumn,
    string ToColumn);
