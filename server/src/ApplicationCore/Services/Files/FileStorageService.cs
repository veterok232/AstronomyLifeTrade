using ApplicationCore.Enums;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.Files;
using ApplicationCore.Models.Files;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Settings;
using Microsoft.Extensions.Options;
using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Services.Files;

[ScopedDependency]
internal class FileStorageService : IFileStorageService
{
    private readonly IFileService _fileService;
    private readonly IFileStorageFactory _fileStorageFactory;
    private readonly ISaveFileValidator _saveFileValidator;
    private readonly IOptions<FileStorageSettings> _fileStorageSettings;
    private readonly IFileAvailabilityValidator _fileAvailabilityValidator;

    public FileStorageService(
        IFileStorageFactory fileStorageFactory,
        IFileService fileService,
        ISaveFileValidator saveFileValidator,
        IOptions<FileStorageSettings> fileStorageSettings,
        IFileAvailabilityValidator fileAvailabilityValidator)
    {
        _fileStorageFactory = fileStorageFactory;
        _fileService = fileService;
        _saveFileValidator = saveFileValidator;
        _fileStorageSettings = fileStorageSettings;
        _fileAvailabilityValidator = fileAvailabilityValidator;
    }

    private FileStorageType DefaultStorageType => _fileStorageSettings.Value.StorageType;

    public async Task<File> SaveWithFileCreation(File file, Stream stream)
    {
        await Save(file, stream);

        return await _fileService.Create(file);
    }

    public async Task<File> Save(File file, Stream stream)
    {
        if (!_saveFileValidator.IsInputDataValid(stream))
        {
            throw new InvalidInputException("Illegal attempt to save file");
        }

        SetStorageType(file);

        IFileStorage storage = _fileStorageFactory.CreateUserStorage(file.StorageType);
        await storage.Save(file.Reference, stream);

        return file;
    }

    public Task<FileDescriptor> Get(File file)
    {
        return DownloadFile(file);
    }

    public async Task<FileDescriptor> Get(Guid fileId)
    {
        File file = await _fileService.GetById(fileId);
        return await DownloadFile(file);
    }

    public async Task Delete(File file)
    {
        var storage = _fileStorageFactory.CreateUserStorage(file.StorageType);
        await storage.Delete(file.Reference);
        await _fileService.Delete(file);
    }

    public async Task<FileDescriptor> GetOwned(Guid fileId)
    {
        File file = await _fileService.GetById(fileId);
        ValidateFile(file, fileId);

        return await DownloadFile(file);
    }

    private void SetStorageType(File file)
    {
        if ((int)file.StorageType == 0)
        {
            file.StorageType = DefaultStorageType;
        }
    }

    private void ValidateFile(File file, Guid fileId)
    {
        if (!_fileAvailabilityValidator.IsAvailable(file))
        {
            throw new PotentiallyConcurrentModificationsException($"Requested file '{fileId}' is not found!");
        }
    }

    private async Task<FileDescriptor> DownloadFile(File file)
    {
        IFileStorage storage = _fileStorageFactory.CreateUserStorage(file.StorageType);

        return new FileDescriptor
        {
            FileName = file.FullFileName,
            Stream = await storage.Get(file.Reference),
            MimeType = file.MimeType,
        };
    }
}