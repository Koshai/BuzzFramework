namespace Buzz.Blazor.Models;

/// <summary>
/// Semantic types used by smart-table formatting and analytics modules.
/// </summary>
public enum BuzzTableDataType
{
    /// <summary>Plain text value.</summary>
    Text = 0,
    /// <summary>Numeric value.</summary>
    Number = 1,
    /// <summary>Currency value.</summary>
    Currency = 2,
    /// <summary>Percentage value.</summary>
    Percent = 3,
    /// <summary>Date-only value.</summary>
    Date = 4,
    /// <summary>Date and time value.</summary>
    DateTime = 5,
    /// <summary>Boolean value.</summary>
    Boolean = 6
}
