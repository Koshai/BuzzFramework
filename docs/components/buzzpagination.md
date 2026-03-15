# BuzzPagination

`BuzzPagination` provides compact reusable paging controls for list and table views.

## Basic usage

```razor
<BuzzPagination
    CurrentPage="@CurrentPage"
    CurrentPageChanged="OnPageChanged"
    TotalPages="12"
    MaxVisiblePages="5"
    ShowEdgeButtons="true" />
```

## Parameters and effects

- `CurrentPage`: active page number.
- `CurrentPageChanged`: callback for page changes.
- `TotalPages`: maximum number of pages.
- `MaxVisiblePages`: number of page buttons shown at once.
- `ShowEdgeButtons`: shows/hides first/last navigation buttons.
