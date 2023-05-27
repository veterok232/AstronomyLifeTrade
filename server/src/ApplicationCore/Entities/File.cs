using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities;

public class File : Entity, IHasCreatedAt
{
    public string FileName { get; set; }

    public string Reference { get; set; }

    public string Extension { get; set; }

    public string MimeType { get; set; }

    public long FileSizeInBytes { get; set; }

    public AttachmentType AttachmentType { get; set; }

    public FileStorageType StorageType { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsAttached { get; set; }

    public string FullFileName => $"{FileName}{Extension}";
    
    public Guid? AssignmentId { get; set; }
    
    public Assignment? Assignment { get; set; }
}