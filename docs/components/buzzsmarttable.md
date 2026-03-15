# BuzzSmartTable

`BuzzSmartTable` renders structured tabular data and can optionally recommend an initial sort column based on context.

## Basic usage

```razor
<BuzzSmartTable
    Label="Open Case Snapshot"
    Columns="@_columns"
    Rows="@_rows"
    Modules="@BuzzSmartTableModules.Analytics"
    ShowSummaryFooter="true"
    ShowInsightsPanel="true"
    ShowAiInsightsPanel="true"
    ShowFilterPanel="true"
    EnableGlobalSearch="true"
    EnableGrouping="true"
    SavedViews="@_savedViews"
    SavedViewsChanged="OnSavedViewsChanged"
    EnableAiRecommendedSort="true"
    AutoRecommendSortOnLoad="true"
    EnableAiInsights="true"
    AutoGenerateAiInsightsOnLoad="true"
    ShowRecommendationHint="true"
    ShowRowCount="true"
    HighlightFirstRow="true"
    SourceText="@_caseSummary" />

@code {
    private readonly IReadOnlyList<BuzzTableColumn> _columns =
    [
        new("case", "Case", BuzzTableDataType.Text, false, null, null, BuzzTableAggregationType.Count),
        new("category", "Category", BuzzTableDataType.Text, false, null, null, BuzzTableAggregationType.DistinctCount),
        new("minutes", "Resolution Minutes", BuzzTableDataType.Number, true, "N1", "en-US", BuzzTableAggregationType.Average),
        new("successRate", "Success Rate", BuzzTableDataType.Percent, true, "P1", "en-US", BuzzTableAggregationType.Average),
        new("updated", "Updated", BuzzTableDataType.DateTime, true, "g", "en-US", BuzzTableAggregationType.Max)
    ];

    private readonly IReadOnlyList<IReadOnlyDictionary<string, string>> _rows =
    [
        new Dictionary<string, string> { ["case"] = "INC-2041", ["category"] = "Session/token refresh", ["minutes"] = "42.5", ["successRate"] = "0.92", ["updated"] = "2026-03-06T09:42:00" },
        new Dictionary<string, string> { ["case"] = "INC-2039", ["category"] = "MFA reset", ["minutes"] = "55.0", ["successRate"] = "0.86", ["updated"] = "2026-03-06T09:15:00" },
        new Dictionary<string, string> { ["case"] = "INC-2035", ["category"] = "Password reset flow", ["minutes"] = "21.0", ["successRate"] = "0.98", ["updated"] = "2026-03-06T08:58:00" }
    ];
    private IReadOnlyList<BuzzSmartTableViewState> _savedViews =
    [
        new("High success focus", GroupByKey: "category", SortKey: "successRate", SortAscending: false)
    ];

    private string _caseSummary = "Login issues increased after security policy update.";

    private Task OnSavedViewsChanged(IReadOnlyList<BuzzSmartTableViewState> views)
    {
        _savedViews = views;
        return Task.CompletedTask;
    }
}
```

## Why this helps customers

- Consolidates case metadata into a scannable view.
- Reduces analysis time with context-aware initial sort suggestion.
- Keeps familiar table behavior (manual sorting by clicking headers).

## Parameters and effects

- `Columns`: Required column definitions (`BuzzTableColumn`).
- `Rows`: Required row dictionaries keyed by column key.
- `Modules`: Use presets (`Basic`, `Analytics`, `Full`) for modular adoption.
- `ShowSummaryFooter`: Shows aggregate footer cells based on each column's `Aggregation`.
- `ShowInsightsPanel`: Shows missing %, duplicate rows, and per-column quality insights.
- `ShowAiInsightsPanel`: Displays AI-generated narrative analysis section.
- `ShowFilterPanel`: Displays local filter controls above the table.
- `EnableGlobalSearch`: Enables search across all columns.
- `EnableGrouping`: Enables group-by dropdown and grouped row-count summary.
- `SavedViews` and `SavedViewsChanged`: Save/reapply table states (sort/filter/group).
- `ShowRowCount`: Displays total row count above the table.
- `HighlightFirstRow`: Emphasizes the first row after sorting.
- `BuzzTableColumn.DataType`: Controls typed parsing and sorting (`Number`, `Percent`, `DateTime`, etc.).
- `BuzzTableColumn.EnableAutoFormat`: Enables formatted display by data type.
- `BuzzTableColumn.Format` and `Culture`: Customize output style and locale per column.
- `EnableAiRecommendedSort`: Enables AI sort recommendation.
- `AutoRecommendSortOnLoad`: Runs recommendation once during load.
- `EnableAiInsights`: Enables AI trend/anomaly/next-action narrative.
- `AutoGenerateAiInsightsOnLoad`: Runs AI analysis automatically on first load.
- `AiInsightFallbackText`: Developer-defined fallback message when AI analysis fails.
- `SourceText`: Context payload for AI sort suggestion.
- `MaxInputCharacters`: Caps AI input length for cost/latency control.
