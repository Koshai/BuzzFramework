# BuzzNavBar

`BuzzNavBar` provides top-level navigation with brand, route links, and optional right-side actions.

## Basic usage

```razor
<BuzzNavBar BrandText="BuzzBlazor"
            BrandHref="/"
            Links="@NavLinks">
    <Actions>
        <a href="https://learn.microsoft.com/aspnet/core/" target="_blank" rel="noreferrer">About Blazor</a>
    </Actions>
</BuzzNavBar>
```

## Parameters and effects

- `BrandText`: brand label on the left.
- `BrandHref`: target for brand link.
- `Links`: collection of `BuzzNavLink(Text, Href, MatchAll)`.
- `Actions`: optional right-side content.
