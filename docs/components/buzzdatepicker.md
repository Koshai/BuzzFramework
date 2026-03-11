# BuzzDatePicker

`BuzzDatePicker` is an AI-assisted date input for scheduling workflows.  
It provides normal date selection plus ranked quick-date suggestions (today, next day, +1 week, etc.) informed by shared memory context.

## Why customers benefit

- Faster follow-up scheduling with one-click date chips.
- More consistent timing choices across similar support cases.
- Explainable recommendation shown under the field.
- Baseline date input still works like a standard control.

## Basic Usage

```razor
<BuzzDatePicker
    Label="Follow-up Date"
    @bind-Value="_followUpDate"
    MemorySubject="support-login-cases"
    ReferenceText="@_issueSummary"
    ShowQuickSuggestions="true"
    ShowRecommendationHint="true" />
```

## Implementation Example

```razor
@page "/date-example"

<BuzzDatePicker
    Label="Follow-up Date"
    @bind-Value="_followUpDate"
    MemorySubject="@_sharedSubject"
    ReferenceText="@_issueSummary"
    Required="true"
    HelperText="Pick the date for final user confirmation."
    ErrorText="@_followUpDateError"
    ShowQuickSuggestions="true"
    ShowRecommendationHint="true" />

<button class="btn btn-sm btn-outline-primary mt-2" @onclick="Validate">Validate</button>

@code {
    private const string _sharedSubject = "support-login-cases";
    private string _issueSummary = "Windows login is slow after policy update.";
    private string _followUpDate = string.Empty;
    private string? _followUpDateError;

    private void Validate()
    {
        _followUpDateError = string.IsNullOrWhiteSpace(_followUpDate)
            ? "Follow-up date is required."
            : null;
    }
}
```

## Parameters

- `Label` (`string`): date field label.
- `Value` (`string`): selected date in `yyyy-MM-dd`.
- `ValueChanged` (`EventCallback<string>`): selected date callback.
- `Disabled` (`bool`): disables input.
- `Required` (`bool`): marks input required.
- `Min` (`string?`): minimum date (`yyyy-MM-dd`).
- `Max` (`string?`): maximum date (`yyyy-MM-dd`).
- `MemorySubject` (`string?`): shared memory scope.
- `ReferenceText` (`string?`): contextual text for ranking.
- `ShowQuickSuggestions` (`bool`, default `true`): shows suggested date chips.
- `ShowRecommendationHint` (`bool`, default `true`): shows recommended date text.
- `SuggestionCount` (`int`, default `5`): max suggestion count.
- `HelperText` (`string?`): helper text below field.
- `ErrorText` (`string?`): validation/error message.
- `DescribedBy` (`string?`): extra ARIA description IDs.

## Notes

- `BuzzDatePicker` uses `IBuzzOptionRanker`, so it reuses your existing memory signals.
- Use `Min` and `Max` for policy windows (for example no earlier than today).
