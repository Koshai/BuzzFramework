# BuzzSideNav

`BuzzSideNav` renders vertical navigation links for dashboard or admin layouts.

## Basic usage

```razor
<BuzzSideNav
    Title="Demo Sections"
    Links="@SideNavLinks" />
```

## Parameters and effects

- `Title`: sidebar heading text.
- `Links`: list of `BuzzNavLink` entries.
- `Label`: accessibility label for nav region.
