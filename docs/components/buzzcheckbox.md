# BuzzCheckBox

`BuzzCheckBox` is a standard checkbox with optional AI-assisted recommendation hints.

It keeps normal checkbox behavior while suggesting likely on/off states based on similar case history (`MemorySubject` + `ReferenceText`).

## Basic Usage

```razor
<BuzzCheckBox
    Label="Require MFA reset"
    @bind-Value="_requireMfaReset" />
```

## Implementation Example

```razor
@page "/risk-controls"

<BuzzTextBox
    Label="Issue Summary"
    @bind-InputText="_issueSummary"
    MemorySubject="support-login-cases"
    SuggestionCount="6" />

<BuzzCheckBox
    Label="Require MFA reset on next login"
    @bind-Value="_requireMfaReset"
    MemorySubject="support-login-cases"
    ReferenceText="@_issueSummary"
    HelperText="Enable this for compromised accounts."
    ErrorText="@_mfaResetError"
    ShowRecommendationHint="true" />

@code {
    private string _issueSummary = string.Empty;
    private bool _requireMfaReset;
    private string? _mfaResetError;
}
```

## Parameters

- `Label`: display label.
- `Value`: checkbox value (two-way bind).
- `Disabled`: disable interaction.
- `Required`: required semantics.
- `MemorySubject`: shared memory topic key.
- `ReferenceText`: upstream context for recommendation.
- `ShowRecommendationHint`: show recommended state with reason/confidence.
- `HelperText`: helper guidance.
- `ErrorText`: validation/error message; sets `aria-invalid`.
- `DescribedBy`: external `aria-describedby` id(s).

## How AI Helps Checkboxes

- Highlights likely toggle states for similar cases.
- Improves consistency across teams for policy-sensitive toggles.
- Speeds up triage by reducing decision friction on repetitive controls.
- Keeps standard checkbox behavior even when recommendation data is unavailable.
