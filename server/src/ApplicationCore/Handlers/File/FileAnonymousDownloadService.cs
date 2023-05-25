using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Files;
using ApplicationCore.Models.Files;
using ApplicationCore.Services.Dependencies.Attributes;
using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Handlers.File;

[ScopedDependency]
internal class FileAnonymousDownloadService : IFileAnonymousDownloadService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IRepository<Entities.File> _fileRepository;

    public FileAnonymousDownloadService(
        IFileStorageService fileStorageService,
        IRepository<Entities.File> fileRepository)
    {
        _fileStorageService = fileStorageService;
        _fileRepository = fileRepository;
    }

    public async Task<FileDescriptor> Download(Guid fileId)
    {
        var file = await _fileRepository.GetById(fileId);

        return await _fileStorageService.Get(file);
    }
}