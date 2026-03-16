namespace Buzz.Blazor.Models;

/// <summary>
/// Represents one plan card shown by <c>BuzzPricingTable</c>.
/// </summary>
/// <param name="Name">Plan name.</param>
/// <param name="Price">Plan price text.</param>
/// <param name="Period">Billing period text (for example, per month).</param>
/// <param name="Features">Feature bullet list displayed for the plan.</param>
/// <param name="IsHighlighted">Highlights the plan as recommended.</param>
/// <param name="CtaText">Call-to-action button label.</param>
public sealed record BuzzPricingPlan(
    string Name,
    string Price,
    string Period,
    IReadOnlyList<string> Features,
    bool IsHighlighted = false,
    string CtaText = "Choose plan");
