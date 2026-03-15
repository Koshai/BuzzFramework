# BuzzCodeBlock

`BuzzCodeBlock` renders readable code snippets with optional line numbers and copy action.

## Basic usage

```razor
<BuzzCodeBlock
    Title="Create a reusable card"
    Language="razor"
    Code="@SampleSnippet"
    ShowLineNumbers="true"
    WrapLines="true"
    EnableCopyButton="true" />
```

## Parameters and effects

- `Code`: snippet content to render.
- `Language`: language label shown in header.
- `ShowLineNumbers`: shows line index for each row.
- `WrapLines`: wraps long lines instead of horizontal scroll.
- `EnableCopyButton`: enables copy-to-clipboard control.
