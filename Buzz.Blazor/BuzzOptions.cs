namespace Buzz.Blazor;

public sealed class BuzzOptions
{
    public string DefaultProviderName { get; set; } = "mock";
    public bool EnableProviderFailover { get; set; } = true;
    public IReadOnlyList<string> ProviderFailoverOrder { get; set; } = ["openai", "ollama", "mock"];
    public string HistoryStorageKey { get; set; } = "buzz.history.v1";
    public int MaxHistoryEntries { get; set; } = 500;
    public bool EnableAiSuggestions { get; set; } = false;
    public int AiMinInputLength { get; set; } = 12;
    public int AiMaxLocalResultsBeforeSkip { get; set; } = 2;
    public int AiCooldownSeconds { get; set; } = 10;
    public int AiCacheTtlSeconds { get; set; } = 180;
    public bool EnableSharedCaseMemory { get; set; } = true;
    public int SharedCaseMemoryMaxEntriesPerSubject { get; set; } = 2000;
}
