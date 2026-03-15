namespace Buzz.Blazor.Models;

public sealed record BuzzToastItem(
    string Title,
    string Message,
    string Severity = "info",
    string SourceText = "",
    bool EnableAiMessageWhenEmpty = false,
    bool AutoGenerateAiWhenEmpty = true,
    string Id = "");
