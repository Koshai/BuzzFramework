# BuzzAccordion

`BuzzAccordion` groups related help or troubleshooting content into collapsible sections.  
It supports normal expand/collapse behavior and optional AI recommendation for which section to open first.

## Why customers benefit

- Keeps long guidance content organized and scannable.
- Reduces operator effort by surfacing likely-relevant section first.
- Works without AI (manual accordion behavior remains fully functional).

## Basic Usage

```razor
<BuzzAccordion
    Label="Support Case Playbook"
    Sections="@_playbookSections"
    AllowMultipleOpen="false"
    EnableAiRecommendedSection="true"
    SourceText="@CaseSummarySource" />
```

## Implementation Example

```razor
@using Buzz.Blazor.Models

<BuzzAccordion
    Label="Support Case Playbook"
    Sections="@_playbookSections"
    AllowMultipleOpen="false"
    EnableAiRecommendedSection="true"
    AutoRecommendOnLoad="true"
    ShowRecommendationHint="true"
    SourceText="@CaseSummarySource" />

@code {
    private readonly IReadOnlyList<BuzzAccordionItem> _playbookSections =
    [
        new("Credential and account checks", "Validate lock state and reset flow.", true),
        new("Session and token invalidation", "Clear stale sessions and reissue token."),
        new("MFA and risk controls", "Enforce MFA reset for suspicious sign-ins."),
        new("Escalation and communication", "Escalate and notify user with timeline.")
    ];

    private string CaseSummarySource =>
        $"Issue Summary: {_issueSummary}\nResolution Category: {_resolutionCategory}";
}
```

## Parameters

- `Label` (`string`): accordion heading text.
- `Sections` (`IReadOnlyList<BuzzAccordionItem>`): section headers/content and initial open state.
- `AllowMultipleOpen` (`bool`, default `true`): set to `false` to keep one section open and collapse others automatically.
- `ShowRecommendationHint` (`bool`, default `true`): shows AI-recommended section label.
- `EnableAiRecommendedSection` (`bool`, default `true`): enables section recommendation.
- `AutoRecommendOnLoad` (`bool`, default `true`): runs recommendation when component loads.
- `SourceText` (`string`): context payload used for recommendation.
- `MaxInputCharacters` (`int`, default `1200`): truncates AI input for cost/latency control.

## Notes

- For deterministic behavior, disable AI recommendation and use `IsInitiallyExpanded` on desired sections.
- Keep section headers short and distinct for better recommendation matching.
