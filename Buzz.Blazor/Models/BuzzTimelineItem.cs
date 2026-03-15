namespace Buzz.Blazor.Models;

public sealed record BuzzTimelineItem(
    string Title,
    string Detail,
    DateTimeOffset Timestamp,
    string Severity = "info");
