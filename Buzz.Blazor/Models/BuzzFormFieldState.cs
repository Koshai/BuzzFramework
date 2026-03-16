namespace Buzz.Blazor.Models;

/// <summary>
/// Describes a single field snapshot consumed by <c>BuzzFormAssistant</c>.
/// </summary>
/// <param name="Key">Stable field identifier.</param>
/// <param name="Label">User-facing field label.</param>
/// <param name="Value">Current field value.</param>
/// <param name="Required">Indicates whether this field must be completed before submit.</param>
public sealed record BuzzFormFieldState(
    string Key,
    string Label,
    string? Value,
    bool Required = false);
