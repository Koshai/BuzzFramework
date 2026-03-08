# BuzzTextBox

`BuzzTextBox` is a subject-aware input component with:

- local history suggestions
- optional AI-enriched suggestions (OpenAI/Ollama via provider routing)
- optional shared case memory across pages/components
- secure password mode

## Basic Usage

```razor
<BuzzTextBox
    Label="Issue Summary"
    @bind-InputText="_issueSummary"
    SuggestionCount="6"
    AddToHistoryOnEnter="true"
    AddToHistoryOnTab="true"
    Placeholder="Describe the issue..." />
```

## Implementation Example

Use this full example in a `.razor` page:

```razor
@page "/ticket-entry"

<h3>Ticket Entry</h3>

<BuzzTextBox
    Label="Issue Summary"
    @bind-InputText="_issueSummary"
    MemorySubject="support-ticket-cases"
    SuggestionCount="6"
    AddToHistoryOnEnter="true"
    AddToHistoryOnTab="true"
    EnableAiSuggestions="true"
    Placeholder="Describe the issue..." />

<BuzzTextBox
    Label="Resolution Notes"
    @bind-InputText="_resolutionNotes"
    MemorySubject="support-ticket-cases"
    ReferenceText="@_issueSummary"
    SuggestionCount="6"
    AddToHistoryOnEnter="true"
    AddToHistoryOnTab="true"
    EnableAiSuggestions="true"
    Placeholder="Write the resolution..." />

@code {
    private string _issueSummary = string.Empty;
    private string _resolutionNotes = string.Empty;
}
```

## Subject-Aware Shared Memory

To share suggestions across related components, use the same `MemorySubject`.

```razor
<BuzzTextBox
    Label="Issue Summary"
    @bind-InputText="_issueSummary"
    MemorySubject="support-login-cases" />

<BuzzTextBox
    Label="Resolution Notes"
    @bind-InputText="_resolutionNotes"
    MemorySubject="support-login-cases"
    ReferenceText="@_issueSummary" />
```

`ReferenceText` helps dependent fields retrieve better shared suggestions.

## Password Mode

```razor
<BuzzTextBox
    Label="Temporary Password"
    @bind-InputText="_tempPassword"
    InputType="password"
    EnableAiSuggestions="false"
    AddToHistoryOnEnter="false"
    AddToHistoryOnTab="false" />
```

Password implementation example:

```razor
<BuzzTextBox
    Label="Temporary Password"
    @bind-InputText="_temporaryPassword"
    InputType="password"
    EnableAiSuggestions="false"
    AddToHistoryOnEnter="false"
    AddToHistoryOnTab="false"
    Placeholder="Password is masked..." />

@code {
    private string _temporaryPassword = string.Empty;
}
```

Password mode behavior:

- renders masked input
- does not save text to history
- does not request AI suggestions

## Parameters

- `Label`: field label and context signal.
- `InputText`: two-way bound value.
- `Placeholder`: input placeholder text.
- `InputType`: `text`, `password`, etc.
- `SuggestionCount`: max suggestions to display.
- `AddToHistoryOnEnter`: save text on Enter.
- `AddToHistoryOnTab`: save text on Tab.
- `EnableAiSuggestions`: enable AI enrichment from providers.
- `AiDebounceMilliseconds`: wait before AI enrichment after typing.
- `MemorySubject`: shared case-memory topic key.
- `ReferenceText`: optional context (for cross-field suggestions).

## Suggestion Flow

1. User types -> local suggestions render immediately.
2. If AI is enabled, textbox waits for debounce.
3. Policy gates AI call (min length, cooldown, local-result threshold, cache).
4. Results are merged and deduplicated.

## Current Limits

- default shared memory store is in-memory (process scoped)
- local history store uses browser localStorage
- for production shared memory, use a persistent store implementation (planned)
