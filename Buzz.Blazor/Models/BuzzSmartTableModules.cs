namespace Buzz.Blazor.Models;

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
    public static BuzzSmartTableModules Basic { get; } = new(
        EnableAiRecommendedSort: false,
        EnableSummaryFooter: false,
        EnableInsightsPanel: false,
        EnableAiInsightsPanel: false,
        EnableFilterPanel: false,
        EnableGlobalSearch: false,
        EnableGrouping: false,
        EnableSavedViews: false);

    public static BuzzSmartTableModules Analytics { get; } = new(
        EnableAiRecommendedSort: true,
        EnableSummaryFooter: true,
        EnableInsightsPanel: true,
        EnableAiInsightsPanel: true,
        EnableFilterPanel: true,
        EnableGlobalSearch: true,
        EnableGrouping: true,
        EnableSavedViews: true);

    public static BuzzSmartTableModules Full { get; } = Analytics;
}
