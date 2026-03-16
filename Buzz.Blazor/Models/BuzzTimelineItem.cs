namespace Buzz.Blazor.Models;

/// <summary>
/// Represents one event in a chronological timeline.
/// </summary>
/// <param name="Title">Event title.</param>
/// <param name="Detail">Event details.</param>
/// <param name="Timestamp">Event timestamp.</param>
/// <param name="Severity">Visual emphasis hint for the event.</param>
public sealed record BuzzTimelineItem(
    string Title,
    string Detail,
    DateTimeOffset Timestamp,
    string Severity = "info");
