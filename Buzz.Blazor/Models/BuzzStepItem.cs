namespace Buzz.Blazor.Models;

public sealed record BuzzStepItem(
    string Title,
    string Description,
    bool IsInitiallyActive = false);
