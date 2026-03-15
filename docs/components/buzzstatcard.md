# BuzzStatCard

`BuzzStatCard` shows metric snapshots with trend and supporting description.

## Basic usage

```razor
<BuzzStatCard
    Label="Auto-Resolved"
    Value="84%"
    TrendText="+4.1%"
    Description="Resolution success this week."
    Variant="success" />
```

## Parameters and effects

- `Label`: metric name.
- `Value`: main value text.
- `TrendText`: trend indicator text.
- `Description`: extra context under value.
- `Variant`: `neutral`, `accent`, `success`, `warning`, `danger`.
