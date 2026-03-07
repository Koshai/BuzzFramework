using System.Text;
using System.Text.Json;
using Buzz.Core;

namespace Buzz.Provider.Ollama;

public sealed class OllamaBuzzProvider : IBuzzProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;
    private readonly OllamaBuzzOptions _options;

    public OllamaBuzzProvider(HttpClient httpClient, OllamaBuzzOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public string Name => "ollama";

    public async Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default)
    {
        var prompt = BuildPrompt(request);
        var payload = JsonSerializer.Serialize(new GenerateRequest(_options.Model, prompt, false), JsonOptions);
        using var message = new HttpRequestMessage(HttpMethod.Post, "generate")
        {
            Content = new StringContent(payload, Encoding.UTF8, "application/json")
        };

        using var response = await _httpClient.SendAsync(message, cancellationToken);
        var raw = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Ollama request failed with status {(int)response.StatusCode}: {raw}");
        }

        var parsed = JsonSerializer.Deserialize<GenerateResponse>(raw, JsonOptions);
        var output = parsed?.Response?.Trim();
        if (string.IsNullOrWhiteSpace(output))
        {
            throw new InvalidOperationException("Ollama returned an empty response.");
        }

        return new BuzzResponse(output, Name, _options.Model);
    }

    private static string BuildPrompt(BuzzRequest request)
    {
        var instruction = request.Instruction.Trim();
        var userText = request.UserText.Trim();
        var context = string.IsNullOrWhiteSpace(request.Context) ? string.Empty : $"\nContext: {request.Context}";
        return $"Instruction: {instruction}\nText: {userText}{context}";
    }

    private sealed record GenerateRequest(string Model, string Prompt, bool Stream);

    private sealed record GenerateResponse(string? Response);
}
