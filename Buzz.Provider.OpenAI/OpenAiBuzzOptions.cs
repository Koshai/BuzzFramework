namespace Buzz.Provider.OpenAI;

public sealed class OpenAiBuzzOptions
{
    public string ApiKey { get; init; } = string.Empty;
    public string Model { get; init; } = "gpt-4o-mini";
}
