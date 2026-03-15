namespace Buzz.Blazor.Models;

public sealed record BuzzTableColumn(
    string Key,
    string Header,
    BuzzTableDataType DataType = BuzzTableDataType.Text,
    bool EnableAutoFormat = false,
    string? Format = null,
    string? Culture = null,
    BuzzTableAggregationType Aggregation = BuzzTableAggregationType.None);
