namespace Buzz.Blazor.Models;

public sealed record BuzzKanbanItem(
    string Id,
    string Title,
    string Description,
    string Column,
    string Severity = "info");
