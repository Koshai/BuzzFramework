namespace Buzz.Blazor.Models;

/// <summary>
/// Represents one toast notification displayed by <c>BuzzToastCenter</c>.
/// </summary>
/// <param name="Title">Toast title text.</param>
/// <param name="Message">Toast message body. Can be empty when AI generation is enabled.</param>
/// <param name="Severity">Toast severity (for example: info, success, warning, error).</param>
/// <param name="SourceText">Optional context text used for AI-generated message content.</param>
/// <param name="EnableAiMessageWhenEmpty">Enables AI message generation when <paramref name="Message"/> is empty.</param>
/// <param name="AutoGenerateAiWhenEmpty">Automatically requests AI output without a manual button click.</param>
/// <param name="Id">Optional stable identifier used by host lists.</param>
public sealed record BuzzToastItem(
    string Title,
    string Message,
    string Severity = "info",
    string SourceText = "",
    bool EnableAiMessageWhenEmpty = false,
    bool AutoGenerateAiWhenEmpty = true,
    string Id = "");
