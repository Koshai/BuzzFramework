# BuzzFormAssistant

`BuzzFormAssistant` helps developers add pre-submit guidance with required-field checks, completion scoring, AI risk insight, and AI draft rewrite support.

## Basic usage

```razor
<BuzzFormAssistant
    Label="Case Submit Assistant"
    Fields="@_fields"
    ShowChecklist="true"
    ShowCompletionScore="true"
    EnableAiRiskInsight="true"
    AutoGenerateRiskInsightOnLoad="true"
    EnableAiRewriteForMessage="true"
    MessageDraft="@_followUpMessage"
    MessageDraftChanged="OnMessageChanged"
    SourceText="@_caseSummary" />

@code {
    private string _followUpMessage = "We are reviewing your case and will update you shortly.";

    private IReadOnlyList<BuzzFormFieldState> _fields =>
    [
        new("issue", "Issue Summary", "User cannot sign in", true),
        new("category", "Resolution Category", "Session/token refresh", true),
        new("escalation", "Escalation Level", "L2 - Support engineer", true),
        new("message", "Follow-up Message", _followUpMessage, true)
    ];

    private Task OnMessageChanged(string value)
    {
        _followUpMessage = value;
        return Task.CompletedTask;
    }

    private string _caseSummary = "Login issue case requiring user-friendly follow-up.";
}
```

## Why this helps customers

- Reduces incomplete submissions by surfacing missing required fields.
- Adds a consistent pre-submit quality gate for support and operations forms.
- Improves end-user communication with one-click rewrite support.

## Parameters and effects

- `Fields`: Required/optional field states (`BuzzFormFieldState`) used for checklist and score.
- `ShowChecklist`: Shows missing required fields list.
- `ShowCompletionScore`: Shows required completion percentage.
- `EnableAiRiskInsight`: Enables AI pre-submit risk/next-step guidance.
- `AutoGenerateRiskInsightOnLoad`: Generates insight automatically on load.
- `EnableAiRewriteForMessage`: Enables AI rewrite flow for `MessageDraft`.
- `MessageDraft` and `MessageDraftChanged`: Two-way value used by rewrite panel.
- `ShowGenerateButtons`: Controls manual AI action buttons visibility.
- `SourceText`: Extra context payload for AI prompts.
