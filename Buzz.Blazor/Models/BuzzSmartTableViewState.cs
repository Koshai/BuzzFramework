namespace Buzz.Blazor.Models;

public sealed record BuzzSmartTableViewState(
    string Name,
    string? GlobalSearch = null,
    string? GroupByKey = null,
    string? SortKey = null,
    bool SortAscending = true,
    IReadOnlyDictionary<string, string>? ColumnFilters = null);
