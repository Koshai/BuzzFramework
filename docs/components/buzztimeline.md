# BuzzTimeline

`BuzzTimeline` shows ordered activity events for a workflow and can optionally recommend the most relevant event to review first.

## Basic usage

```razor
<BuzzTimeline
    Label="Case Activity Timeline"
    Items="@_timeline"
    EnableAiRecommendedEvent="true"
    AutoRecommendOnLoad="true"
    ShowRecommendationHint="true"
    SortDescending="true"
    SourceText="@_caseSummary" />

@code {
    private readonly IReadOnlyList<BuzzTimelineItem> _timeline =
    [
        new("User reported repeated login failure", "Corporate login fails after password update.", DateTimeOffset.Now.AddMinutes(-35), "warning"),
        new("Session invalidation executed", "Stale sessions cleared and tokens revoked.", DateTimeOffset.Now.AddMinutes(-20), "info"),
        new("MFA reset applied", "Challenge reset completed for user.", DateTimeOffset.Now.AddMinutes(-10), "success")
    ];

    private string _caseSummary = "User cannot sign in after policy update.";
}
```

## Why this helps customers

- Makes case history easy to scan in one place.
- Improves triage speed by highlighting the likely key event first.
- Keeps baseline timeline behavior even when AI is unavailable.

## Parameters and effects

- `Items`: Required timeline entries (`BuzzTimelineItem`).
- `SortDescending`: Controls latest-first or oldest-first order.
- `EnableAiRecommendedEvent`: Enables AI recommendation for one focus event.
- `AutoRecommendOnLoad`: Runs recommendation once when component loads.
- `ShowRecommendationHint`: Shows recommendation text to user.
- `SourceText`: Context payload used by AI recommendation.
- `MaxInputCharacters`: Limits context size for cost and latency control.
