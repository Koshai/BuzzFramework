namespace Buzz.Blazor.Models;

public sealed record BuzzFormFieldState(
    string Key,
    string Label,
    string? Value,
    bool Required = false);
