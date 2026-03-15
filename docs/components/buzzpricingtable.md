# BuzzPricingTable

`BuzzPricingTable` renders responsive pricing cards with feature lists and plan selection callbacks.

## Basic usage

```razor
<BuzzPricingTable
    Plans="@PricingPlans"
    OnPlanSelected="OnPlanSelected" />
```

## Parameters and effects

- `Plans`: list of `BuzzPricingPlan` entries.
- `OnPlanSelected`: callback with selected plan.
