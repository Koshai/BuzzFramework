# BuzzTooltip

`BuzzTooltip` provides quick inline help on hover/focus.
It supports two modes:
- Normal tooltip: developer sets `Message`.
- AI tooltip: when `Message` is empty and AI mode is enabled, it explains raw technical text (`SourceText`) in user-friendly language.

## Why customers benefit

- Reduces confusion around technical terms and codes.
- Keeps UI clean while still providing contextual help.
- Lets developers keep manual explanations where needed.
- Improves UX for non-technical end users.

## Basic Usage

```razor
<BuzzTooltip Message="Escalation routes cases to specialized teams." Placement="right">
    <span class="badge text-bg-secondary">Escalation Help</span>
</BuzzTooltip>
```

## AI Explanation Example

```razor
<BuzzTooltip
    Message=""
    SourceText="HTTP 200 OK indicates that the request succeeded and the server returned the expected response payload."
    EnableAiMessageWhenEmpty="true"
    AutoGenerateAiWhenEmpty="true"
    Placement="right">
    <span class="badge text-bg-info">Explain Technical Status</span>
</BuzzTooltip>
```

## Parameters

- `Message` (`string`): developer-provided tooltip text.
- `SourceText` (`string`): raw technical text used when AI mode is enabled.
- `Placement` (`string`, default `"top"`): `top`, `bottom`, `left`, or `right`.
- `ChildContent` (`RenderFragment?`): trigger content.
- `EnableAiMessageWhenEmpty` (`bool`, default `false`): enables AI generation when `Message` is empty.
- `AutoGenerateAiWhenEmpty` (`bool`, default `true`): generates explanation on open.
- `AiFallbackMessage` (`string`): fallback text if AI fails.
- `TabIndex` (`int`, default `0`): keyboard focus order for accessibility.

## Notes

- Tooltip opens on both hover and keyboard focus.
- For sensitive values, avoid passing full raw payloads to `SourceText`.
