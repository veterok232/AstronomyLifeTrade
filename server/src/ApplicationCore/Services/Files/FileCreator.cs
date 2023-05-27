using ApplicationCore.Interfaces.Files;
using ApplicationCore.Models.Files;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Files;

[ScopedDependency]
internal class FileCreator : IFileCreator
{
    private readonly IFileStorageService _fileStorageService;

    public FileCreator(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task<Entities.File> CreateFile(FileData fileData)
    {
        await using Stream fileStream = fileData.FileModel.OpenReadStream();
        var file = GenerateFile(fileStream, fileData);

        return await _fileStorageService.SaveWithFileCreation(file, fileStream);
    }

    private static Entities.File GenerateFile(
        Stream fileStream,
        FileData createUploadedFileData)
    {
        return new FileBuilder()
            .WithName(Path.GetFileNameWithoutExtension(createUploadedFileData.FileModel.FileName))
            .WithMimeType(createUploadedFileData.FileModel.MimeType)
            .WithExtension(Path.GetExtension(createUploadedFileData.FileModel.FileName))
            .WithAttachmentType(createUploadedFileData.AttachmentType)
            .WithSize(fileStream.Length)
            .Build();
    }
}