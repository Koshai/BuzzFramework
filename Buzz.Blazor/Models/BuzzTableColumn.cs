namespace Buzz.Blazor.Models;

/// <summary>
/// Defines a column schema used by <c>BuzzSmartTable</c>.
/// </summary>
/// <param name="Key">Unique column key used to map row values.</param>
/// <param name="Header">Header text shown in the table.</param>
/// <param name="DataType">Semantic data type used for formatting and aggregation behavior.</param>
/// <param name="EnableAutoFormat">Automatically formats values based on <paramref name="DataType"/>.</param>
/// <param name="Format">Optional explicit format string.</param>
/// <param name="Culture">Optional culture name used during formatting.</param>
/// <param name="Aggregation">Summary aggregation to apply for this column.</param>
public sealed record BuzzTableColumn(
    string Key,
    string Header,
    BuzzTableDataType DataType = BuzzTableDataType.Text,
    bool EnableAutoFormat = false,
    string? Format = null,
    string? Culture = null,
    BuzzTableAggregationType Aggregation = BuzzTableAggregationType.None);
