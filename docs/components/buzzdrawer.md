# BuzzDrawer

`BuzzDrawer` provides slide-in contextual panels for secondary tasks.

## Basic usage

```razor
<BuzzDrawer @bind-IsOpen="_isDrawerOpen" Title="Case Inspector" Position="right">
    <ChildContent>
        <p>Drawer body content</p>
    </ChildContent>
    <Actions>
        <BuzzButton Text="Close" Variant="primary" OnClick="CloseDrawer" />
    </Actions>
</BuzzDrawer>
```

## Parameters and effects

- `IsOpen`: controls drawer visibility.
- `Title`: header text.
- `Position`: `right` or `left`.
- `ChildContent`: drawer body.
- `Actions`: optional footer action content.
