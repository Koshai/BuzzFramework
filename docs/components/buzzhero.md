# BuzzHero

`BuzzHero` builds marketing-style hero sections with CTA actions and optional AI tagline generation.

## Basic usage

```razor
<BuzzHero
    Badge="2026-ready design system"
    Title="Ship support portals faster"
    Subtitle="Compose accessible and AI-assisted workflows."
    EnableAiTagline="true"
    SourceContext="@CaseSummarySource" />
```

## Parameters and effects

- `Title` / `Subtitle`: hero message content.
- `PrimaryCtaText` / `SecondaryCtaText`: CTA labels.
- `EnableAiTagline`: enables AI subtitle generation.
- `SourceContext`: context passed to AI prompt.
