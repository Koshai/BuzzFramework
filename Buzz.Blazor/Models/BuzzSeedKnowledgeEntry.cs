namespace Buzz.Blazor.Models;

/// <summary>
/// Represents a baseline domain-knowledge entry used to improve AI output quality from initial deployment.
/// </summary>
/// <param name="Subject">Subject bucket (for example, sprint-planner, support-ticket).</param>
/// <param name="Component">Optional component key this entry applies to.</param>
/// <param name="Title">Short heading used for diagnostics and traceability.</param>
/// <param name="Text">Seed text included in prompt context.</param>
/// <param name="Tags">Optional search tags used for lightweight relevance matching.</param>
public sealed record BuzzSeedKnowledgeEntry(
    string Subject,
    string? Component,
    string Title,
    string Text,
    IReadOnlyList<string>? Tags);
