namespace Buzz.Blazor.Models;

/// <summary>
/// Represents one slide item for <c>BuzzCarousel</c>.
/// </summary>
/// <param name="Title">Primary heading shown on the slide.</param>
/// <param name="Description">Supporting slide content.</param>
/// <param name="Badge">Optional badge text rendered near the title.</param>
public sealed record BuzzCarouselSlide(
    string Title,
    string Description,
    string? Badge = null);
