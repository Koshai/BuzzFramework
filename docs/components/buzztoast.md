# BuzzToast

`BuzzToast` is a notification component with two modes:
- Normal mode: developer provides `Message` directly.
- AI explanation mode: when `Message` is empty and AI mode is enabled, the component translates raw status/error text into user-friendly language.

## Why customers benefit

- Clear, fast feedback for success/warning/error outcomes.
- Developers keep full control for known messages.
- Raw logs/status codes can be translated for non-technical users.
- Works safely without AI when developer message is provided.

## Basic Usage

```razor
<BuzzToast
    Title="Save Failed"
    Message=""
    @bind-IsVisible="_isToastVisible"
    Severity="error"
    SourceText="@_rawErrorLog"
    EnableAiMessageWhenEmpty="true"
    AutoGenerateAiWhenEmpty="true"
    AutoHide="true"
    UseAdaptiveAutoHide="true"
    AutoHideMilliseconds="5000" />
```

## Implementation Example

```razor
<button class="btn btn-sm btn-outline-primary me-2" @onclick="ShowSuccessToast">
    Show Success Toast
</button>
<button class="btn btn-sm btn-outline-danger" @onclick="ShowErrorToast">
    Show Error Toast (AI explanation)
</button>

<BuzzToast
    Title="@_toastTitle"
    Message="@_toastMessage"
    @bind-IsVisible="_isToastVisible"
    Severity="@_toastSeverity"
    SourceText="@_toastSourceText"
    EnableAiMessageWhenEmpty="@_toastUseAiWhenEmpty"
    AutoGenerateAiWhenEmpty="true"
    AutoHide="true"
    UseAdaptiveAutoHide="true"
    AutoHideMilliseconds="5000" />

@code {
    private bool _isToastVisible;
    private string _toastTitle = "Case Saved";
    private string _toastMessage = "Case draft has been saved successfully.";
    private string _toastSeverity = "success";
    private string _toastSourceText = string.Empty;
    private bool _toastUseAiWhenEmpty;

    private void ShowSuccessToast()
    {
        _toastTitle = "Case Saved";
        _toastMessage = "Case draft has been saved successfully.";
        _toastSeverity = "success";
        _toastSourceText = "HTTP 200 OK. Case draft persisted and workflow state updated.";
        _toastUseAiWhenEmpty = false;
        _isToastVisible = true;
    }

    private void ShowErrorToast()
    {
        _toastTitle = "Save Failed (Raw Log)";
        _toastMessage = string.Empty;
        _toastSeverity = "error";
        _toastSourceText = "System.Data.SqlClient.SqlException: Timeout expired while updating CaseDraft.";
        _toastUseAiWhenEmpty = true;
        _isToastVisible = true;
    }
}
```

## Parameters

- `Title` (`string`): toast title.
- `Message` (`string`): developer-provided message; if set, this is always shown.
- `IsVisible` (`bool`): controls visibility.
- `IsVisibleChanged` (`EventCallback<bool>`): visibility binding callback.
- `Severity` (`string`, default `"info"`): `success`, `info`, `warning`, or `error`.
- `EnableAiMessageWhenEmpty` (`bool`, default `false`): enables AI generation when `Message` is empty.
- `AutoGenerateAiWhenEmpty` (`bool`, default `true`): auto-generates AI explanation when toast opens.
- `SourceText` (`string`): raw status/log/context text used by AI.
- `AiFallbackMessage` (`string`): fallback text when AI generation fails.
- `AutoHide` (`bool`, default `true`): auto-dismisses toast.
- `AutoHideMilliseconds` (`int`, default `4500`): auto-dismiss delay.
- `UseAdaptiveAutoHide` (`bool`, default `false`): computes hide time from message length.
- `AdaptiveMinMilliseconds` (`int`, default `2500`): minimum adaptive timeout.
- `AdaptiveMaxMilliseconds` (`int`, default `10000`): maximum adaptive timeout.
- `AdaptiveBaseMilliseconds` (`int`, default `1200`): fixed base added before reading-time estimate.
- `AdaptiveWordReadMilliseconds` (`int`, default `280`): per-word read-time estimate.
- `ShowCloseButton` (`bool`, default `true`): shows close icon.

## Notes

- Recommended pattern: pass explicit message for expected outcomes and use AI mode for technical/raw payloads.
- Keep `SourceText` short and relevant to control latency/cost.
- Adaptive mode is helpful when AI-generated explanations vary in length.
