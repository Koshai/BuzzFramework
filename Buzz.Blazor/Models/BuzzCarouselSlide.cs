namespace Buzz.Blazor.Models;

public sealed record BuzzCarouselSlide(
    string Title,
    string Description,
    string? Badge = null);
