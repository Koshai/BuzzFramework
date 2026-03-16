namespace Buzz.Blazor.Models;

/// <summary>
/// Defines a section rendered by <c>BuzzAccordion</c>.
/// </summary>
/// <param name="Header">Display title for the accordion trigger.</param>
/// <param name="Content">Body text shown when the section is expanded.</param>
/// <param name="IsInitiallyExpanded">When <see langword="true"/>, expands this section on first render.</param>
public sealed record BuzzAccordionItem(
    string Header,
    string Content,
    bool IsInitiallyExpanded = false);
