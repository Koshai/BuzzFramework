# BuzzStack

`BuzzStack` is a flex layout primitive for arranging child content vertically or horizontally with configurable spacing.

## Basic usage

```razor
<BuzzStack Direction="horizontal" Gap="0.5rem" Wrap="true" Align="center">
    <BuzzBadge Text="A" />
    <BuzzBadge Text="B" />
</BuzzStack>
```

## Parameters and effects

- `Direction`: `vertical` or `horizontal`.
- `Gap`: CSS spacing value between items.
- `Align`: cross-axis alignment.
- `Justify`: main-axis alignment.
- `Wrap`: enables wrapping in horizontal mode.
