namespace Buzz.Core;

/// <summary>
/// Represents a normalized AI generation request sent to an <see cref="IBuzzProvider"/>.
/// </summary>
/// <param name="UserText">Primary user text to process.</param>
/// <param name="Instruction">Provider instruction that guides generation behavior.</param>
/// <param name="Context">Optional supporting context appended to the request.</param>
public sealed record BuzzRequest(
    string UserText,
    string Instruction,
    string? Context = null);
