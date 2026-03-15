# BuzzTabs

`BuzzTabs` displays related content panels in a compact space while allowing optional AI-assisted tab recommendation based on context.

## Basic usage

```razor
<BuzzTabs
    Label="Case Resolution Views"
    Tabs="@_tabs"
    EnableAiRecommendedTab="true"
    AutoRecommendOnLoad="true"
    ShowRecommendationHint="true"
    SourceText="@_caseSummary" />

@code {
    private readonly IReadOnlyList<BuzzTabItem> _tabs =
    [
        new("Executive summary", "High-level overview.", true),
        new("Technical detail", "Investigation and telemetry notes."),
        new("Customer communication", "User-facing explanation.")
    ];

    private string _caseSummary = "User login failures increased after policy update.";
}
```

## Why this helps customers

- Keeps workflows focused by grouping related views in one component.
- Improves discoverability by surfacing the most relevant tab first.
- Preserves baseline tab behavior even when AI is unavailable.

## Parameters and effects

- `Tabs`: Required tab definitions (`BuzzTabItem`) to render.
- `ActiveTab` and `ActiveTabChanged`: Control selected tab from parent state.
- `EnableAiRecommendedTab`: Enables AI tab recommendation.
- `AutoRecommendOnLoad`: Triggers recommendation automatically on first load.
- `ShowRecommendationHint`: Displays recommended tab text for user context.
- `SourceText`: Context payload used by AI recommendation.
- `MaxInputCharacters`: Limits AI input size for cost and latency control.
