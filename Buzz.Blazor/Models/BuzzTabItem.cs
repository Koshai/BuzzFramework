namespace Buzz.Blazor.Models;

public sealed record BuzzTabItem(
    string Header,
    string Content,
    bool IsInitiallyActive = false);
