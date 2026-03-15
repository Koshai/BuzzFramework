# BuzzList

`BuzzList` renders ordered or unordered item lists with optional selection callbacks.

## Basic usage

```razor
<BuzzList
    Items="@ChecklistItems"
    Ordered="true"
    Selectable="true"
    OnItemSelected="OnChecklistItemSelected" />
```

## Parameters and effects

- `Items`: list item values.
- `Ordered`: when true, renders an ordered list.
- `Selectable`: enables click behavior.
- `OnItemSelected`: callback with selected item value.
