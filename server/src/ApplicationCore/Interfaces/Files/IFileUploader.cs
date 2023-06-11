using ApplicationCore.Enums;
using ApplicationCore.Models.Files;
using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Interfaces.Files;

public interface IFileUploader
{
    Task<File> Upload(ReadableFileModel model, AttachmentType attachmentType, bool isAttached = true);
}