namespace Buzz.Blazor.Models;

/// <summary>
/// Defines one step in the <c>BuzzStepper</c> workflow.
/// </summary>
/// <param name="Title">Step title text.</param>
/// <param name="Description">Step body description.</param>
/// <param name="IsInitiallyActive">Marks this step as initially selected.</param>
public sealed record BuzzStepItem(
    string Title,
    string Description,
    bool IsInitiallyActive = false);
