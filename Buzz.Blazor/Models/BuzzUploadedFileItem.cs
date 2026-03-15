namespace Buzz.Blazor.Models;

public sealed record BuzzUploadedFileItem(
    string Name,
    long SizeBytes,
    string ContentType);
