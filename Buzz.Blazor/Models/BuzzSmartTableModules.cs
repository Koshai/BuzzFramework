namespace Buzz.Blazor.Models;

/// <summary>
/// Feature toggle set that controls which modules are active in <c>BuzzSmartTable</c>.
/// </summary>
/// <param name="EnableAiRecommendedSort">Enables AI-driven sort recommendations.</param>
/// <param name="EnableSummaryFooter">Shows summary rows and aggregate values.</param>
/// <param name="EnableInsightsPanel">Shows deterministic insight panel.</param>
/// <param name="EnableAiInsightsPanel">Shows AI-generated insight panel.</param>
/// <param name="EnableFilterPanel">Shows per-column filtering controls.</param>
/// <param name="EnableGlobalSearch">Shows global search input.</param>
/// <param name="EnableGrouping">Enables grouping by selected columns.</param>
/// <param name="EnableSavedViews">Enables saving and applying table view states.</param>
public sealed record BuzzSmartTableModules(
    bool EnableAiRecommendedSort = true,
    bool EnableSummaryFooter = true,
    bool EnableInsightsPanel = true,
    bool EnableAiInsightsPanel = true,
    bool EnableFilterPanel = true,
    bool EnableGlobalSearch = true,
    bool EnableGrouping = true,
    bool EnableSavedViews = true)
{
    /// <summary>
    /// Minimal module configuration for simple table use cases.
    /// </summary>
    public static BuzzSmartTableModules Basic { get; } = new(
        EnableAiRecommendedSort: false,
        EnableSummaryFooter: false,
        EnableInsightsPanel: false,
        EnableAiInsightsPanel: false,
        EnableFilterPanel: false,
        EnableGlobalSearch: false,
        EnableGrouping: false,
        EnableSavedViews: false);

    /// <summary>
    /// Full analytics-oriented configuration with all table modules enabled.
    /// </summary>
    public static BuzzSmartTableModules Analytics { get; } = new(
        EnableAiRecommendedSort: true,
        EnableSummaryFooter: true,
        EnableInsightsPanel: true,
        EnableAiInsightsPanel: true,
        EnableFilterPanel: true,
        EnableGlobalSearch: true,
        EnableGrouping: true,
        EnableSavedViews: true);

    /// <summary>
    /// Alias for <see cref="Analytics"/>.
    /// </summary>
    public static BuzzSmartTableModules Full { get; } = Analytics;
}
