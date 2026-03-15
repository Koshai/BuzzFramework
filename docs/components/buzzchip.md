# BuzzChip

`BuzzChip` displays compact tags for filters, labels, and removable metadata.

## Basic usage

```razor
<BuzzChip
    Text="Priority P2"
    Variant="accent"
    Removable="true"
    OnRemove="HandleRemoveAsync" />
```

## Parameters and effects

- `Text`: chip label text.
- `Variant`: `neutral`, `accent`, `success`, `warning`, `danger`.
- `LeadingIcon`: optional icon text.
- `Removable`: shows remove action button.
- `OnClick`: callback when chip body is clicked.
- `OnRemove`: callback when remove action is triggered.
