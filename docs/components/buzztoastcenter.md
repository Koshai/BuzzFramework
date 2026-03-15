# BuzzToastCenter

`BuzzToastCenter` orchestrates multiple toasts in a fixed stack.

## Basic usage

```razor
<BuzzToastCenter
    Items="@ToastItems"
    ItemsChanged="OnToastItemsChanged" />
```

## Parameters and effects

- `Items`: list of `BuzzToastItem` messages.
- `ItemsChanged`: callback for updated active toast list.
- `Label`: accessibility label for the toast region.
