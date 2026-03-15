# BuzzKanbanBoard

`BuzzKanbanBoard` organizes workflow cards into lanes and supports quick movement between lanes with optional AI lane recommendation.

## Basic usage

```razor
<BuzzKanbanBoard
    Label="Support Workflow Lanes"
    Columns="@_columns"
    Items="@_items"
    ItemsChanged="OnItemsChanged"
    OnItemMoved="OnItemMoved"
    ShowCounts="true"
    AllowMoveButtons="true"
    MoveLeftButtonText="Move backward"
    MoveRightButtonText="Move forward"
    EnableDragAndDrop="true"
    EnableAiRecommendedColumn="true"
    AutoRecommendOnLoad="true"
    ShowRecommendationHint="true"
    SourceText="@_caseSummary" />

@code {
    private readonly IReadOnlyList<string> _columns =
    [
        "Backlog",
        "In Progress",
        "Review",
        "Done"
    ];

    private IReadOnlyList<BuzzKanbanItem> _items =
    [
        new("K-1001", "Investigate token refresh failures", "Validate stale session handling.", "Backlog", "warning"),
        new("K-1002", "Run MFA reset workflow", "Apply controlled MFA reset.", "In Progress", "info"),
        new("K-1003", "Verify communication draft", "Review user-facing status message.", "Review", "success")
    ];

    private Task OnItemsChanged(IReadOnlyList<BuzzKanbanItem> items)
    {
        _items = items;
        return Task.CompletedTask;
    }

    private Task OnItemMoved(BuzzKanbanMoveEvent move)
    {
        Console.WriteLine($"{move.ItemId}: {move.FromColumn} -> {move.ToColumn}");
        return Task.CompletedTask;
    }

    private string _caseSummary = "Login incidents need triage and closure planning.";
}
```

## Why this helps customers

- Makes workflow status visible at a glance.
- Reduces task coordination friction with lane move controls.
- Adds AI guidance on which lane should be prioritized.

## Parameters and effects

- `Columns`: lane names and order.
- `Items` and `ItemsChanged`: card state source and callback.
- `OnItemMoved`: movement telemetry callback with item id and from/to lanes.
- `ShowCounts`: shows card count in each lane header.
- `AllowMoveButtons`: enables per-card move left/right controls.
- `MoveLeftButtonText` and `MoveRightButtonText`: custom labels for lane movement actions.
- `EnableDragAndDrop`: allows direct drag-and-drop card movement between lanes.
- `EnableAiRecommendedColumn`: enables AI focus-lane recommendation.
- `AutoRecommendOnLoad`: auto-runs recommendation once at load.
- `ShowRecommendationHint`: displays recommended lane text.
- `SourceText`: contextual input for AI recommendation.
