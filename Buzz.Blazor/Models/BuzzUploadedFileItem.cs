namespace Buzz.Blazor.Models;

/// <summary>
/// Describes an uploaded file tracked by Buzz upload components.
/// </summary>
/// <param name="Name">File name.</param>
/// <param name="SizeBytes">File size in bytes.</param>
/// <param name="ContentType">MIME content type.</param>
public sealed record BuzzUploadedFileItem(
    string Name,
    long SizeBytes,
    string ContentType);
