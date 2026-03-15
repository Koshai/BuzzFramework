# BuzzDropdownMenu

`BuzzDropdownMenu` provides compact contextual menus for user actions.

## Basic usage

```razor
<BuzzDropdownMenu
    Label="Case actions"
    Items="@MenuItems"
    OnItemSelected="OnMenuSelected" />
```

## Parameters and effects

- `Label`: trigger button text.
- `Items`: list of `BuzzMenuItem(Text, Value, Disabled)`.
- `OnItemSelected`: selected item callback.
