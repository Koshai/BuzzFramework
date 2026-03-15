# BuzzDataPanel

`BuzzDataPanel` presents key metrics with context, highlight value, and optional child detail content.

## Basic usage

```razor
<BuzzDataPanel
    Title="Incident Resolution Throughput"
    Subtitle="Last 24 hours"
    PrimaryValue="84%"
    Description="Auto-resolution rate improved after token refresh fix deployment."
    BadgeText="Stable"
    Variant="success">
    <BuzzBadge Text="Resolved: 126" Variant="success" />
</BuzzDataPanel>
```

## Parameters and effects

- `Title`: panel title text.
- `Subtitle`: supporting line under title.
- `PrimaryValue`: highlighted metric value.
- `Description`: explanatory message.
- `BadgeText`: optional status badge in header.
- `Variant`: `neutral`, `accent`, `success`, `warning`.
- `ChildContent`: optional extra inline details.
