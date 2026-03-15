# BuzzCodeEditor

`BuzzCodeEditor` provides a focused code editing surface with optional AI suggestions.

## Basic usage

```razor
<BuzzCodeEditor
    Label="Resolution helper function"
    Language="csharp"
    @bind-Value="_codeDraft"
    Height="220px"
    EnableAiAssist="true"
    SourceContext="@CaseSummarySource" />
```

## Parameters and effects

- `Value`: current code content.
- `ValueChanged`: callback for editor changes.
- `EnableAiAssist`: enables AI improvement generation.
- `AiInstruction`: custom prompt used for AI transformation.
- `SourceContext`: domain context sent with AI request.
- `MaxInputCharacters`: max characters used in AI calls.
