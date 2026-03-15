# BuzzButton

`BuzzButton` is a foundational action component with variant, size, loading state, and click callback support.

## Basic usage

```razor
<BuzzButton
    Text="Save"
    Variant="primary"
    Size="md"
    OnClick="HandleSaveAsync" />
```

## Parameters and effects

- `Text`: button label.
- `Variant`: `primary`, `secondary`, `outline`, `danger`.
- `Size`: `sm`, `md`, `lg`.
- `Loading`: disables interaction and shows spinner.
- `Disabled`: disables button state.
- `OnClick`: click callback event.
