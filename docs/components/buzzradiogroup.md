# BuzzRadioGroup

`BuzzRadioGroup` is an AI-assisted radio selection component for mutually exclusive choices.
It keeps expected radio behavior while ranking options using case memory and typed context.

## Why customers benefit

- Faster selection: the most probable option appears at the top.
- Better consistency: repeated support cases converge on known resolution paths.
- Explainable guidance: each recommendation can show a short reason.
- Safer UX: still works as a normal radio group even when AI/memory signals are limited.

## Basic Usage

```razor
<BuzzRadioGroup
    Label="Preferred Resolution Path"
    @bind-Value="_resolutionPath"
    Options="@_resolutionPaths"
    MemorySubject="support-login-cases"
    ReferenceText="@_issueSummary"
    ShowRecommendationHint="true"
    ShowRankingReasons="true" />
```

## Implementation Example

```razor
@page "/radio-example"

<h2>BuzzRadioGroup Example</h2>

<BuzzRadioGroup
    Label="Preferred Resolution Path"
    @bind-Value="_resolutionPath"
    Options="@_resolutionPaths"
    MemorySubject="@_sharedSubject"
    ReferenceText="@_issueSummary"
    Required="true"
    HelperText="Pick one path for final execution."
    ErrorText="@_resolutionPathError"
    ShowRecommendationHint="true"
    ShowRankingReasons="true" />

<button class="btn btn-sm btn-outline-primary mt-2" @onclick="Validate">Validate</button>

@code {
    private const string _sharedSubject = "support-login-cases";
    private string _issueSummary = "User cannot login after password reset.";
    private string _resolutionPath = string.Empty;
    private string? _resolutionPathError;

    private readonly IReadOnlyList<string> _resolutionPaths =
    [
        "Immediate password reset and re-login",
        "Force token/session refresh",
        "MFA reset and identity re-verification",
        "Role and permission synchronization",
        "Escalate to identity specialist"
    ];

    private void Validate()
    {
        _resolutionPathError = string.IsNullOrWhiteSpace(_resolutionPath)
            ? "Resolution path is required."
            : null;
    }
}
```

## Parameters

- `Label` (`string`): radio group label.
- `Options` (`IReadOnlyList<string>`): available radio options.
- `Value` (`string`): selected option value.
- `ValueChanged` (`EventCallback<string>`): selected value callback.
- `Disabled` (`bool`): disables the full group.
- `Required` (`bool`): marks group selection as required.
- `MaxVisibleOptions` (`int`, default `8`): max options rendered after ranking.
- `MemorySubject` (`string?`): shared memory scope for cross-case learning.
- `ReferenceText` (`string?`): contextual text used for ranking relevance.
- `ShowRecommendationHint` (`bool`, default `true`): shows recommended option hint.
- `ShowRankingReasons` (`bool`, default `true`): shows reason next to each ranked option.
- `HelperText` (`string?`): helper text under the group.
- `ErrorText` (`string?`): validation/error message text.
- `DescribedBy` (`string?`): extra ARIA description IDs.

## Notes

- `BuzzRadioGroup` uses the same ranking engine as `BuzzComboBox` and `BuzzSelectBox`.
- If the current selected value is outside `Options`, it is still shown as "Current selection."
- For stronger recommendations, provide both `MemorySubject` and meaningful `ReferenceText`.
