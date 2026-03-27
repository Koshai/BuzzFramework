using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Buzz.Core;

namespace Buzz.Provider.OpenAI;

public sealed class OpenAiBuzzProvider : IBuzzProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;
    private readonly OpenAiBuzzOptions _options;

    public OpenAiBuzzProvider(HttpClient httpClient, OpenAiBuzzOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public string Name => "openai";

    public async Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            throw new InvalidOperationException("OpenAI API key is missing.");
        }

        using var message = CreateRequestMessage(request);
        using var response = await _httpClient.SendAsync(message, cancellationToken);

        var payload = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"OpenAI request failed with status {(int)response.StatusCode}: {payload}");
        }

        var parsed = JsonSerializer.Deserialize<ChatCompletionsResponse>(payload, JsonOptions);
        var output = parsed?.Choices?.FirstOrDefault()?.Message?.Content?.Trim();
        if (string.IsNullOrWhiteSpace(output))
        {
            throw new InvalidOperationException("OpenAI returned an empty response.");
        }

        return new BuzzResponse(output, Name, _options.Model);
    }

    private HttpRequestMessage CreateRequestMessage(BuzzRequest request)
    {
        var baseInstruction = request.Instruction.Trim();
        var userText = request.UserText.Trim();
        var context = string.IsNullOrWhiteSpace(request.Context) ? string.Empty : $"\nContext: {request.Context}";
        var prompt = $"Instruction: {baseInstruction}\nText: {userText}{context}";

        var body = new ChatCompletionsRequest(
            _options.Model,
            [
                new ChatMessage("system", "You are Buzz, a concise writing assistant."),
                new ChatMessage("user", prompt)
            ],
            Math.Max(32, _options.MaxOutputTokens),
            Math.Clamp(_options.Temperature, 0, 2));

        var message = new HttpRequestMessage(HttpMethod.Post, "chat/completions");
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
        message.Content = new StringContent(JsonSerializer.Serialize(body, JsonOptions), Encoding.UTF8, "application/json");
        return message;
    }

    private sealed record ChatCompletionsRequest(
        string Model,
        IReadOnlyList<ChatMessage> Messages,
        [property: JsonPropertyName("max_tokens")]
        int MaxTokens,
        double Temperature);

    private sealed record ChatMessage(string Role, string Content);

    private sealed record ChatCompletionsResponse(IReadOnlyList<Choice>? Choices);

    private sealed record Choice(ChatMessage? Message);
}
