namespace Buzz.Blazor.Models;

/// <summary>
/// Defines one tab entry rendered by <c>BuzzTabs</c>.
/// </summary>
/// <param name="Header">Tab button text.</param>
/// <param name="Content">Tab panel content text.</param>
/// <param name="IsInitiallyActive">Marks this tab as initially active.</param>
public sealed record BuzzTabItem(
    string Header,
    string Content,
    bool IsInitiallyActive = false);
