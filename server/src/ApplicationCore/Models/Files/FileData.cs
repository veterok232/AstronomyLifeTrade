using ApplicationCore.Enums;

namespace ApplicationCore.Models.Files;

public class FileData
{
    public ReadableFileModel FileModel { get; set; }

    public AttachmentType AttachmentType { get; set; }
}