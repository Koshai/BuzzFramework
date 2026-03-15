# BuzzConfirmDialog

`BuzzConfirmDialog` asks users to confirm risky or destructive actions.

## Basic usage

```razor
<BuzzConfirmDialog
    @bind-IsOpen="_isConfirmOpen"
    Title="Delete draft?"
    Message="This action permanently removes the current draft. Continue?"
    ConfirmButtonText="Delete draft"
    CancelButtonText="Keep draft"
    ConfirmVariant="danger"
    OnConfirm="OnConfirmDelete"
    OnCancel="OnCancelDelete" />
```

## Parameters and effects

- `IsOpen`: controls dialog visibility.
- `Title` / `Message`: dialog context text.
- `ConfirmVariant`: style for primary action (`danger`, etc.).
- `OnConfirm` / `OnCancel`: action callbacks.
