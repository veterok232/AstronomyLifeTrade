namespace ApplicationCore.Models.Files;

public sealed class FileDescriptor : IDisposable
{
    public string FileName { get; set; }

    public Stream Stream { get; set; }

    public string MimeType { get; set; }

    public void Dispose()
    {
        Stream?.Dispose();
    }
}