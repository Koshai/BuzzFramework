namespace Buzz.Blazor.Models;

public sealed record BuzzAccordionItem(
    string Header,
    string Content,
    bool IsInitiallyExpanded = false);
