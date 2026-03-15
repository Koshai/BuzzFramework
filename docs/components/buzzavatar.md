# BuzzAvatar

`BuzzAvatar` shows a user identity with initials or image and optional presence status.

## Basic usage

```razor
<BuzzAvatar
    Name="Case Owner"
    Variant="accent"
    Size="md"
    ShowStatusDot="true"
    Status="online" />
```

## Parameters and effects

- `Name`: source for initials and tooltip label.
- `ImageUrl`: optional image source.
- `Size`: `sm`, `md`, `lg`.
- `Shape`: `circle`, `rounded`.
- `Variant`: `neutral`, `accent`.
- `ShowStatusDot`: toggles status indicator.
- `Status`: `online`, `offline`, `busy`.
