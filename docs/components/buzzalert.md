# BuzzAlert

`BuzzAlert` displays inline status messages for info, success, warning, and error flows.

## Basic usage

```razor
<BuzzAlert
    Title="Case needs manual verification"
    Message="Automatic checks found conflicting identity signals."
    Variant="warning"
    Dismissible="true"
    @bind-IsVisible="_isAlertVisible" />
```

## Parameters and effects

- `Title`: short heading text for the alert.
- `Message`: supporting explanation text.
- `Variant`: `info`, `success`, `warning`, `danger`.
- `Dismissible`: adds close button.
- `IsVisible`: controls rendered visibility (supports two-way binding).
- `OnDismiss`: callback fired when user dismisses.
