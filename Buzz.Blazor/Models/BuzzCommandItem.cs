namespace Buzz.Blazor.Models;

public sealed record BuzzCommandItem(
    string Title,
    string Description,
    string Value);
