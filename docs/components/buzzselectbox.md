# BuzzSelectBox

`BuzzSelectBox` is a standard single-select dropdown enhanced with context-aware option ranking.

It keeps baseline select behavior while promoting options that were frequently selected in similar subjects and reference contexts.

## Basic Usage

```razor
<BuzzSelectBox
    Label="Escalation Level"
    @bind-Value="_escalationLevel"
    Options="@_escalationLevels"
    PlaceholderOptionText="Choose escalation level..." />
```

## Implementation Example

Use this full example in a `.razor` page:

```razor
@page "/triage"

<BuzzTextBox
    Label="Issue Summary"
    @bind-InputText="_issueSummary"
    MemorySubject="support-login-cases"
    SuggestionCount="6" />

<BuzzSelectBox
    Label="Escalation Level"
    @bind-Value="_escalationLevel"
    MemorySubject="support-login-cases"
    ReferenceText="@_issueSummary"
    Options="@_escalationLevels"
    MaxVisibleOptions="5"
    PlaceholderOptionText="Choose escalation level..."
    ShowRecommendationHint="true" />

@code {
    private string _issueSummary = string.Empty;
    private string _escalationLevel = string.Empty;

    private readonly IReadOnlyList<string> _escalationLevels =
    [
        "L1 - Self-service",
        "L2 - Support engineer",
        "L3 - Identity specialist",
        "L4 - Platform on-call",
        "Critical incident"
    ];
}
```

## Parameters

- `Label`: display label and context signal.
- `Value`: selected value (two-way bind).
- `Options`: candidate options to rank and display.
- `Disabled`: disables control.
- `Required`: applies required validation behavior.
- `ShowPlaceholderOption`: show an empty option on top.
- `PlaceholderOptionText`: placeholder option text.
- `MaxVisibleOptions`: max options after ranking.
- `MemorySubject`: shared case-memory topic key.
- `ReferenceText`: upstream context for ranking.
- `ShowRecommendationHint`: show the top recommended option below the control.
- `HelperText`: optional helper guidance shown below the select.
- `ErrorText`: optional validation/error message; also sets `aria-invalid`.
- `DescribedBy`: append external `aria-describedby` id(s).

## Accessibility + Validation Example

```razor
<BuzzSelectBox
    Label="Escalation Level"
    @bind-Value="_escalationLevel"
    Options="@_escalationLevels"
    Required="true"
    HelperText="Required for routing"
    ErrorText="@_errorText"
    ShowRecommendationHint="true" />

@code {
    private string _escalationLevel = string.Empty;
    private string? _errorText;
}
```

## Why It Helps Customers

- Faster triage decisions because likely escalation choices surface earlier.
- Better consistency because similar cases bias toward the same support path.
- Lower operational errors by keeping normal select behavior with smarter ordering.
