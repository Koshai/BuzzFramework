# BuzzModal

`BuzzModal` is an AI-assisted dialog component for review and confirmation flows.  
It provides standard modal behavior with optional AI insight generation from contextual text.

## Why customers benefit

- Adds a final decision checkpoint before submit.
- Summarizes context inside a focused dialog instead of full-page scanning.
- Optional AI insight helps operators make faster, safer decisions.
- Baseline dialog behavior still works even without AI.

## Basic Usage

```razor
<button @onclick="() => _open = true">Open</button>

<BuzzModal
    Title="Final Review"
    @bind-IsOpen="_open"
    SourceText="@_reviewText"
    EnableAiInsight="true"
    AutoGenerateOnOpen="true">
    <ChildContent>
        <p>Review key values before submitting.</p>
    </ChildContent>
</BuzzModal>
```

## Implementation Example

```razor
@page "/modal-example"

<button class="btn btn-primary" @onclick="OpenDialog">Open Case Review</button>

<BuzzModal
    Title="Final Case Review"
    @bind-IsOpen="_isOpen"
    SourceText="@CaseSummarySource"
    EnableAiInsight="true"
    AutoGenerateOnOpen="true"
    CloseOnBackdropClick="true">
    <ChildContent>
        <p><strong>Issue:</strong> @_issueSummary</p>
        <p><strong>Category:</strong> @_resolutionCategory</p>
        <p><strong>Escalation:</strong> @_escalationLevel</p>
    </ChildContent>
</BuzzModal>

@code {
    private bool _isOpen;
    private string _issueSummary = "User cannot login after policy update.";
    private string _resolutionCategory = "Session/token refresh";
    private string _escalationLevel = "L2 - Support engineer";

    private string CaseSummarySource =>
        $"Issue Summary: {_issueSummary}\n" +
        $"Resolution Category: {_resolutionCategory}\n" +
        $"Escalation Level: {_escalationLevel}";

    private void OpenDialog() => _isOpen = true;
}
```

## Parameters

- `Title` (`string`): dialog title.
- `IsOpen` (`bool`): controls visibility.
- `IsOpenChanged` (`EventCallback<bool>`): two-way binding callback.
- `ChildContent` (`RenderFragment?`): body content.
- `Actions` (`RenderFragment?`): footer actions; if omitted, a default close button is shown.
- `CloseOnBackdropClick` (`bool`, default `true`): closes modal when clicking overlay.
- `EnableAiInsight` (`bool`, default `true`): enables AI section.
- `AutoGenerateOnOpen` (`bool`, default `false`): auto-runs AI insight when modal opens.
- `SourceText` (`string`): text used for AI insight generation.
- `CloseButtonText` (`string`, default `"Close"`): default footer button label.

## Notes

- `BuzzModal` uses the shared `IBuzzClient` provider pipeline (OpenAI/Ollama/mock based on config).
- Keep `SourceText` concise for faster and cheaper provider calls.
