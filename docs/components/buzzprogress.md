# BuzzProgress

`BuzzProgress` renders linear completion state for forms, workflows, and task progress.

## Basic usage

```razor
<BuzzProgress
    Label="Form completion"
    Value="@_progressValue"
    Max="100"
    Variant="accent"
    ShowValueLabel="true" />
```

## Parameters and effects

- `Label`: optional title above the progress track.
- `Value`: current value.
- `Max`: maximum value used for percent calculation.
- `Variant`: `accent`, `success`, `warning`, `danger`.
- `ShowValueLabel`: shows calculated percentage text.
