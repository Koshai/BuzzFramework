# BuzzBreadcrumb

`BuzzBreadcrumb` renders path navigation for location context and quick back-navigation.

## Basic usage

```razor
<BuzzBreadcrumb
    Items="@BreadcrumbItems"
    ShowHome="true"
    HomeText="Dashboard"
    Selectable="true"
    OnItemSelected="OnBreadcrumbSelected" />
```

## Parameters and effects

- `Items`: breadcrumb path values.
- `ShowHome`: adds optional root entry.
- `HomeText`: root entry label.
- `Selectable`: allows clicking non-terminal crumbs.
- `OnItemSelected`: callback with selected crumb text.
