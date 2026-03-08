# BuzzComboBox

`BuzzComboBox` is a ranked option selector that adapts to recurring cases.

Compared to basic comboboxes, it doesn't only filter by text. It can also promote options that were successful in similar contexts (same subject + related reference text).

The dropdown shows lightweight "why-ranked" hints such as:

- `Frequent in similar cases`
- `Best typed match`
- `Related typed match`

## Basic Usage

```razor
<BuzzComboBox
    Label="Resolution Category"
    @bind-Value="_resolutionCategory"
    Options="@_resolutionCategories"
    Placeholder="Select or type a category..." />
```

## Implementation Example

Use this full example in a `.razor` page:

```razor
@page "/resolution-routing"

<h3>Resolution Routing</h3>

<BuzzTextBox
    Label="Issue Summary"
    @bind-InputText="_issueSummary"
    MemorySubject="support-login-cases"
    SuggestionCount="6" />

<BuzzComboBox
    Label="Resolution Category"
    @bind-Value="_resolutionCategory"
    MemorySubject="support-login-cases"
    ReferenceText="@_issueSummary"
    Options="@_resolutionCategories"
    MaxVisibleOptions="6"
    ShowRankingReasons="true"
    AllowCustomValues="true"
    Placeholder="Select or type a category..." />

@code {
    private string _issueSummary = string.Empty;
    private string _resolutionCategory = string.Empty;

    private readonly IReadOnlyList<string> _resolutionCategories =
    [
        "Password reset flow",
        "Session/token refresh",
        "Multi-factor verification",
        "Account unlock"
    ];
}
```

## Subject-Aware Usage

```razor
<BuzzComboBox
    Label="Resolution Category"
    @bind-Value="_resolutionCategory"
    MemorySubject="support-login-cases"
    ReferenceText="@_issueSummary"
    Options="@_resolutionCategories" />
```

## Parameters

- `Label`: display label and context signal.
- `Value`: selected/typed value (two-way bind).
- `Options`: candidate options to rank and display.
- `Placeholder`: input placeholder.
- `MaxVisibleOptions`: max options shown in dropdown.
- `MemorySubject`: shared case-memory topic key.
- `ReferenceText`: upstream context used for relevance.
- `ShowRankingReasons`: show/hide explanation badges in dropdown.
- `AllowCustomValues`: allow typed values that are not in predefined options.

## Custom Entries

By default, users can type a value and press `Enter` or `Tab` to keep it as a selected custom value, just like a standard editable combobox.

If you want strict select-only behavior:

```razor
<BuzzComboBox
    Label="Resolution Category"
    @bind-Value="_resolutionCategory"
    Options="@_resolutionCategories"
    AllowCustomValues="false" />
```

## Why It Helps Customers

- Faster selection for frequent workflows because common resolutions rise to the top.
- More consistent outcomes across teams because similar cases bias toward proven categories.
- Lower training burden for new agents since dropdown order reflects real usage patterns.
- Better UX under large option sets because ranking is context-aware, not just alphabetical.
