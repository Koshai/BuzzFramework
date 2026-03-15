# BuzzStepper

`BuzzStepper` guides users through ordered workflow steps and can optionally recommend the most relevant step based on context.

## Basic usage

```razor
<BuzzStepper
    Label="Case Handling Workflow"
    Steps="@_steps"
    ActiveStep="@_activeStep"
    ActiveStepChanged="OnStepChanged"
    EnableAiRecommendedStep="true"
    AutoRecommendOnLoad="true"
    ShowRecommendationHint="true"
    SourceText="@_caseSummary" />

@code {
    private string _activeStep = string.Empty;
    private string _caseSummary = "User cannot sign in after MFA reset.";
    private readonly IReadOnlyList<BuzzStepItem> _steps =
    [
        new("Triage incoming case", "Validate user identity and reproduce issue.", true),
        new("Apply first resolution", "Run the highest-confidence remediation."),
        new("Verify with user", "Confirm successful login and expected behavior."),
        new("Close and document", "Record cause and prevention notes.")
    ];

    private Task OnStepChanged(string step)
    {
        _activeStep = step;
        return Task.CompletedTask;
    }
}
```

## Why this helps customers

- Improves case consistency by making workflow stages explicit.
- Reduces missed steps with visible progress and structured navigation.
- Uses context to highlight the likely next best step when AI is enabled.

## Parameters and effects

- `Steps`: Required step definitions (`BuzzStepItem`) to render.
- `ActiveStep` and `ActiveStepChanged`: Control/observe selected step in parent state.
- `AllowStepNavigation`: Enables or disables manual click navigation on steps.
- `EnableAiRecommendedStep`: Enables AI step recommendation.
- `AutoRecommendOnLoad`: Runs recommendation once on initial load.
- `ShowRecommendationHint`: Displays recommended step hint to users.
- `SourceText`: Context payload used for AI recommendation.
- `MaxInputCharacters`: Caps AI input size for latency/cost control.
