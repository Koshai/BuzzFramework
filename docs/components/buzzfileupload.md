# BuzzFileUpload

`BuzzFileUpload` supports multi-file intake with limits, validation hints, and optional AI insight.

## Basic usage

```razor
<BuzzFileUpload
    Label="Attach case evidence files"
    AllowMultiple="true"
    MaxFiles="5"
    MaxFileSizeMb="8"
    EnableAiInsight="true"
    SourceContext="@CaseSummarySource"
    FilesChanged="OnFilesChanged" />
```

## Parameters and effects

- `AllowMultiple`: enables selecting multiple files.
- `MaxFiles`: cap for uploaded file count.
- `MaxFileSizeMb`: individual file size limit.
- `EnableAiInsight`: enables AI file-list analysis.
- `FilesChanged`: callback with `BuzzUploadedFileItem` metadata.
