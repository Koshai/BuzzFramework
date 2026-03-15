# BuzzEmptyState

`BuzzEmptyState` presents a no-data message and optional guided primary action.

## Basic usage

```razor
<BuzzEmptyState
    Title="No escalated items"
    Description="There are no P1/P2 escalations in this queue."
    Icon="!"
    PrimaryActionText="Create escalation"
    PrimaryAction="CreateEscalationAsync" />
```

## Parameters and effects

- `Title`: primary heading text.
- `Description`: supporting explanation.
- `Icon`: optional icon text.
- `PrimaryActionText`: button label.
- `PrimaryAction`: callback for the primary action.
