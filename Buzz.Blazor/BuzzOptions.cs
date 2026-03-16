namespace Buzz.Blazor;

/// <summary>
/// Global framework options for Buzz Blazor services and AI behavior.
/// </summary>
public sealed class BuzzOptions
{
    /// <summary>
    /// Preferred provider name used for first attempt selection.
    /// </summary>
    public string DefaultProviderName { get; set; } = "mock";
    /// <summary>
    /// Enables failover attempts when the preferred provider fails.
    /// </summary>
    public bool EnableProviderFailover { get; set; } = true;
    /// <summary>
    /// Ordered provider names used when failover is enabled.
    /// </summary>
    public IReadOnlyList<string> ProviderFailoverOrder { get; set; } = ["openai", "ollama", "mock"];
    /// <summary>
    /// Browser storage key for persisted suggestion history.
    /// </summary>
    public string HistoryStorageKey { get; set; } = "buzz.history.v1";
    /// <summary>
    /// Maximum number of history entries retained in storage.
    /// </summary>
    public int MaxHistoryEntries { get; set; } = 500;
    /// <summary>
    /// Enables AI-augmented suggestion expansion.
    /// </summary>
    public bool EnableAiSuggestions { get; set; } = false;
    /// <summary>
    /// Minimum input length required before AI suggestions are considered.
    /// </summary>
    public int AiMinInputLength { get; set; } = 12;
    /// <summary>
    /// Skips AI calls when local suggestions already exceed this threshold.
    /// </summary>
    public int AiMaxLocalResultsBeforeSkip { get; set; } = 2;
    /// <summary>
    /// Cooldown period between AI suggestion calls in seconds.
    /// </summary>
    public int AiCooldownSeconds { get; set; } = 10;
    /// <summary>
    /// Suggestion cache TTL in seconds.
    /// </summary>
    public int AiCacheTtlSeconds { get; set; } = 180;
    /// <summary>
    /// Enables in-memory shared case memory across components.
    /// </summary>
    public bool EnableSharedCaseMemory { get; set; } = true;
    /// <summary>
    /// Maximum entries retained per memory subject bucket.
    /// </summary>
    public int SharedCaseMemoryMaxEntriesPerSubject { get; set; } = 2000;
}
