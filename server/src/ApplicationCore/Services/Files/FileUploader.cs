using ApplicationCore.Enums;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Files;
using ApplicationCore.Models.Files;
using ApplicationCore.Services.Dependencies.Attributes;
using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Services.Files;

[ScopedDependency]
internal class FileUploader : IFileUploader
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IAuthContextAccessor _authContextAccessor;

    public FileUploader(
        IFileStorageService fileStorageService,
        IAuthContextAccessor authContextAccessor)
    {
        _fileStorageService = fileStorageService;
        _authContextAccessor = authContextAccessor;
    }

    public async Task<File> Upload(ReadableFileModel model, AttachmentType attachmentType, bool isAttached = true)
    {
        await using Stream fileStream = model.OpenReadStream();

        var file = new FileBuilder()
            .WithName(Path.GetFileNameWithoutExtension(model.FileName))
            .WithMimeType(model.MimeType)
            .WithExtension(Path.GetExtension(model.FileName))
            .WithAttachmentType(attachmentType)
            .WithSize(fileStream.Length)
            .IsAttached(isAttached)
            .Build();

        return await _fileStorageService.SaveWithFileCreation(file, fileStream);
    }
}