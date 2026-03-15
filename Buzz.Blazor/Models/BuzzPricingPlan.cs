namespace Buzz.Blazor.Models;

public sealed record BuzzPricingPlan(
    string Name,
    string Price,
    string Period,
    IReadOnlyList<string> Features,
    bool IsHighlighted = false,
    string CtaText = "Choose plan");
