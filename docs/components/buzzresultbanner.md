# BuzzResultBanner

`BuzzResultBanner` highlights operation outcomes with optional action and dismiss controls.

## Basic usage

```razor
<BuzzResultBanner
    Title="Batch update completed"
    Message="11 records updated successfully. 1 record requires manual follow-up."
    Variant="warning"
    ActionText="Review flagged item"
    Dismissible="true"
    @bind-IsVisible="_isBannerVisible"
    OnAction="OnBannerAction" />
```

## Parameters and effects

- `Title`: primary outcome title.
- `Message`: supporting details.
- `Variant`: `info`, `success`, `warning`, `danger`.
- `ActionText`: optional action button text.
- `Dismissible`: enables dismiss button.
- `IsVisible`: banner visibility state (supports two-way binding).
- `OnAction`: callback for action button.
- `OnDismiss`: callback when dismissed.
