using System.Text.Json;
using Buzz.Blazor.Models;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Buzz.Blazor.Services;

internal sealed class LocalStorageBuzzHistoryStore : IBuzzHistoryStore
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly IJSRuntime _jsRuntime;
    private readonly BuzzOptions _options;

    public LocalStorageBuzzHistoryStore(IJSRuntime jsRuntime, IOptions<BuzzOptions> options)
    {
        _jsRuntime = jsRuntime;
        _options = options.Value;
    }

    public async Task<IReadOnlyList<BuzzHistoryItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var raw = await _jsRuntime.InvokeAsync<string?>(
                "localStorage.getItem",
                cancellationToken,
                _options.HistoryStorageKey);

            if (string.IsNullOrWhiteSpace(raw))
            {
                return [];
            }

            var parsed = JsonSerializer.Deserialize<List<BuzzHistoryItem>>(raw, JsonOptions);
            return parsed ?? [];
        }
        catch (JSException)
        {
            return [];
        }
    }

    public async Task SaveAllAsync(IReadOnlyList<BuzzHistoryItem> items, CancellationToken cancellationToken = default)
    {
        try
        {
            var payload = JsonSerializer.Serialize(items, JsonOptions);
            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                cancellationToken,
                _options.HistoryStorageKey,
                payload);
        }
        catch (JSException)
        {
            // Skip persistence when JS runtime is not available.
        }
    }
}
