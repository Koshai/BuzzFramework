# BuzzCard

`BuzzCard` is a standard content card with optional AI-generated insights.  
It lets developers keep normal card behavior (title, content, actions) while adding contextual summaries for better operator productivity.

## Why customers benefit

- Faster decision making: turns long case details into a short summary.
- Better consistency: suggests a concrete next step in support workflows.
- Safe fallback: if AI is unavailable, the card still renders normal content.
- Developer-friendly: no complex setup beyond standard Buzz provider configuration.

## Basic Usage

```razor
<BuzzCard
    Title="Case Overview"
    Subtitle="AI-assisted support snapshot"
    SourceText="@CaseSourceText"
    EnableAiSummary="true"
    ShowGenerateButton="true">
    <ChildContent>
        <p><strong>Issue:</strong> @_issueSummary</p>
        <p><strong>Resolution:</strong> @_resolutionNotes</p>
    </ChildContent>
    <Actions>
        <button class="btn btn-sm btn-outline-secondary">Open ticket</button>
    </Actions>
</BuzzCard>
```

## Implementation Example

```razor
@page "/card-example"

<h2>BuzzCard Example</h2>

<BuzzCard
    Title="Support Case Summary"
    Subtitle="Generated from form inputs"
    SourceText="@CaseSourceText"
    EnableAiSummary="true"
    AutoGenerateOnChange="false"
    ShowGenerateButton="true"
    GenerateButtonText="Generate AI Summary">
    <ChildContent>
        <p><strong>Issue Summary:</strong> @_issueSummary</p>
        <p><strong>Category:</strong> @_resolutionCategory</p>
        <p><strong>Escalation:</strong> @_escalationLevel</p>
    </ChildContent>
    <Actions>
        <button class="btn btn-sm btn-outline-primary">Save Snapshot</button>
    </Actions>
</BuzzCard>

@code {
    private string _issueSummary = "User cannot login after password reset.";
    private string _resolutionCategory = "Password reset flow";
    private string _escalationLevel = "L2 - Support engineer";

    private string CaseSourceText =>
        $"Issue Summary: {_issueSummary}\n" +
        $"Resolution Category: {_resolutionCategory}\n" +
        $"Escalation Level: {_escalationLevel}";
}
```

## Parameters

- `Title` (`string`): card title text.
- `Subtitle` (`string?`): optional subtitle under title.
- `ChildContent` (`RenderFragment?`): main card body.
- `Actions` (`RenderFragment?`): footer area for action controls.
- `EnableAiSummary` (`bool`, default `true`): turns AI insight section on/off.
- `AutoGenerateOnChange` (`bool`, default `false`): auto-generates when `SourceText` changes.
- `ShowGenerateButton` (`bool`, default `true`): shows button for manual generation.
- `GenerateButtonText` (`string`, default `"Generate AI Summary"`): button label.
- `SourceText` (`string`): text payload used for AI insight generation.
- `MaxInputCharacters` (`int`, default `1400`): trims source text before request.
- `UseLocalFallbackSummary` (`bool`, default `true`): creates local summary if provider fails.

## Notes

- `BuzzCard` uses the same provider pipeline as other Buzz components (`OpenAI -> Ollama -> mock`, depending on your configuration).
- Keep `SourceText` concise and focused for faster responses and lower cost.
- For sensitive data, sanitize `SourceText` before passing it to the component.
