# BuzzFooter

`BuzzFooter` creates a reusable bottom-of-page section with brand and link groups.

## Basic usage

```razor
<BuzzFooter
    BrandText="Buzz Framework"
    Description="AI-ready Blazor components for modern web development."
    Links="@FooterLinks"
    Copyright="@($"Copyright {DateTime.UtcNow.Year} Buzz Framework")" />
```

## Parameters and effects

- `BrandText`: footer brand label.
- `Description`: supporting text.
- `Links`: list of `BuzzNavLink` entries.
- `Copyright`: legal copy text.
