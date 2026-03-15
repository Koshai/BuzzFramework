# BuzzDateRangePicker

`BuzzDateRangePicker` manages date-window selection with preset shortcuts and optional AI guidance.

## Basic usage

```razor
<BuzzDateRangePicker
    Label="Review date range"
    Start="@RangeStart"
    StartChanged="OnRangeStartChanged"
    End="@RangeEnd"
    EndChanged="OnRangeEndChanged"
    RangeChanged="OnRangeChanged"
    EnableAiRecommendedRange="true"
    SourceContext="@CaseSummarySource" />
```

## Parameters and effects

- `Start` / `End`: selected date boundaries.
- `StartChanged` / `EndChanged`: controlled value callbacks.
- `RangeChanged`: full range callback (`BuzzDateRange`).
- `EnableAiRecommendedRange`: enables AI range recommendations.
