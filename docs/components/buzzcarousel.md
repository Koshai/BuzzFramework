# BuzzCarousel

`BuzzCarousel` cycles through content slides for onboarding and marketing highlight sections.

## Basic usage

```razor
<BuzzCarousel
    Slides="@Slides"
    ActiveIndexChanged="OnActiveIndexChanged" />
```

## Parameters and effects

- `Slides`: list of `BuzzCarouselSlide` entries.
- `ActiveIndexChanged`: callback with current slide index.
- `Label`: accessibility label for the carousel region.
