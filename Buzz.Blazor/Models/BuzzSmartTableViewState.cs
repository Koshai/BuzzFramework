namespace Buzz.Blazor.Models;

/// <summary>
/// Captures persisted state for a named smart-table view.
/// </summary>
/// <param name="Name">Display name for the saved view.</param>
/// <param name="GlobalSearch">Optional global search query.</param>
/// <param name="GroupByKey">Optional column key used for grouping.</param>
/// <param name="SortKey">Optional column key used for sorting.</param>
/// <param name="SortAscending">Sort direction when <paramref name="SortKey"/> is provided.</param>
/// <param name="ColumnFilters">Optional map of column key to filter value.</param>
public sealed record BuzzSmartTableViewState(
    string Name,
    string? GlobalSearch = null,
    string? GroupByKey = null,
    string? SortKey = null,
    bool SortAscending = true,
    IReadOnlyDictionary<string, string>? ColumnFilters = null);
