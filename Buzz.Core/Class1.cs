namespace Buzz.Core;

public sealed record BuzzRequest(
    string UserText,
    string Instruction,
    string? Context = null);
