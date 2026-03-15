# BuzzCommandPalette

`BuzzCommandPalette` provides quick search and selection for predefined actions, with optional AI recommendation for the best command to run first.

## Basic usage

```razor
<BuzzCommandPalette
    Label="Case Action Commands"
    Commands="@_commands"
    Query="@_query"
    QueryChanged="OnQueryChanged"
    SelectedCommand="@_selected"
    SelectedCommandChanged="OnSelectedChanged"
    EnableAiRecommendedCommand="true"
    AutoRecommendOnLoad="true"
    ShowRecommendationHint="true"
    SourceText="@_caseSummary" />

@code {
    private string _query = string.Empty;
    private string _selected = string.Empty;
    private string _caseSummary = "User repeatedly fails login after policy update.";
    private readonly IReadOnlyList<BuzzCommandItem> _commands =
    [
        new("Reset password and notify user", "Secure reset and notification flow.", "password-reset"),
        new("Invalidate all sessions", "Clear active sessions and revoke tokens.", "invalidate-sessions"),
        new("Enforce MFA reset", "Require re-enrollment on next sign in.", "mfa-reset")
    ];

    private Task OnQueryChanged(string value)
    {
        _query = value;
        return Task.CompletedTask;
    }

    private Task OnSelectedChanged(string value)
    {
        _selected = value;
        return Task.CompletedTask;
    }
}
```

## Why this helps customers

- Speeds up frequent operations by reducing navigation overhead.
- Keeps action choices discoverable with contextual search.
- Provides AI-assisted command prioritization without breaking baseline behavior.

## Parameters and effects

- `Commands`: Required command list (`BuzzCommandItem`).
- `Query` and `QueryChanged`: Controls search query from parent.
- `SelectedCommand` and `SelectedCommandChanged`: Returns selected command value.
- `MaxVisibleCommands`: Limits rendered commands for compact UX.
- `SyncQueryWithSelection`: Updates query text to selected command title when true.
- `EnableAiRecommendedCommand`: Enables AI recommendation hint.
- `AutoRecommendOnLoad`: Runs recommendation once on first load.
- `SourceText` and `MaxInputCharacters`: AI context and size constraint.
