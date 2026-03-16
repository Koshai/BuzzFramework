namespace Buzz.Blazor.Models;

/// <summary>
/// Supported aggregation strategies for <c>BuzzSmartTable</c> summary calculations.
/// </summary>
public enum BuzzTableAggregationType
{
    /// <summary>No aggregation.</summary>
    None = 0,
    /// <summary>Count all rows.</summary>
    Count = 1,
    /// <summary>Count distinct values.</summary>
    DistinctCount = 2,
    /// <summary>Sum numeric values.</summary>
    Sum = 3,
    /// <summary>Average numeric values.</summary>
    Average = 4,
    /// <summary>Minimum value.</summary>
    Min = 5,
    /// <summary>Maximum value.</summary>
    Max = 6
}
