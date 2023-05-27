namespace ApplicationCore.Models.Files;

public class ReadableFileModel
{
    public string FileName { get; set; }

    public string MimeType { get; set; }

    public Func<Stream> OpenReadStream { get; set; }
}