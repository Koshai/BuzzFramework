using Microsoft.Extensions.Configuration;

namespace Buzz.Blazor;

/// <summary>
/// Global framework options for Buzz Blazor services and AI behavior.
/// </summary>
public sealed class BuzzOptions
{
    /// <summary>
    /// Preferred provider name used for first attempt selection.
    /// </summary>
    /// <remarks>Binds from appsettings "Buzz:DefaultProvider" when using configuration.</remarks>
    [ConfigurationKeyName("DefaultProvider")]
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
    /// Hard cap for AI prompt/context payload length to reduce token spend.
    /// </summary>
    public int AiMaxPromptCharacters { get; set; } = 1800;
    /// <summary>
    /// Maximum user-input characters included in AI suggestion prompts.
    /// </summary>
    public int AiMaxUserInputCharacters { get; set; } = 350;
    /// <summary>
    /// Maximum AI generation requests allowed per UTC day.
    /// Set to 0 or less to disable daily request limits.
    /// </summary>
    public int AiMaxRequestsPerDay { get; set; } = 0;
    /// <summary>
    /// Behavior when the daily AI request budget is exceeded.
    /// Supported values: <c>throw</c>, <c>fallback-mock</c>.
    /// </summary>
    public string AiBudgetExceededBehavior { get; set; } = "throw";
    /// <summary>
    /// Enables in-memory shared case memory across components.
    /// </summary>
    public bool EnableSharedCaseMemory { get; set; } = true;
    /// <summary>
    /// Maximum entries retained per memory subject bucket.
    /// </summary>
    public int SharedCaseMemoryMaxEntriesPerSubject { get; set; } = 2000;
    /// <summary>
    /// Enables composition of AI prompts from seed knowledge and user memory.
    /// </summary>
    public bool EnableAiContextEnrichment { get; set; } = true;
    /// <summary>
    /// Enables loading baseline seed knowledge from a JSON file.
    /// </summary>
    public bool EnableSeedKnowledgeBootstrap { get; set; } = true;
    /// <summary>
    /// Relative or absolute path to the seed knowledge JSON file.
    /// </summary>
    public string SeedKnowledgeFilePath { get; set; } = "seed/buzz-seed-knowledge.json";
    /// <summary>
    /// Maximum number of seed entries included in one AI request context.
    /// </summary>
    public int SeedKnowledgeMaxMatchesPerRequest { get; set; } = 4;
    /// <summary>
    /// Maximum number of recent user-memory entries included in one AI request context.
    /// </summary>
    public int UserMemoryMaxMatchesPerRequest { get; set; } = 5;
    /// <summary>
    /// Enables startup warmup so seed knowledge is available before first user interaction.
    /// </summary>
    public bool EnableSeedKnowledgeWarmupOnStartup { get; set; } = true;
}

