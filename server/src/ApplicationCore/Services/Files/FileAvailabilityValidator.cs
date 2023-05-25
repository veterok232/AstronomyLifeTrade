using ApplicationCore.Interfaces.Files;
using ApplicationCore.Services.Dependencies.Attributes;
using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Services.Files;

[ScopedDependency]
internal class FileAvailabilityValidator : IFileAvailabilityValidator
{
    private readonly IFileService _fileService;

    public FileAvailabilityValidator(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<bool> IsAvailable(Guid fileId)
    {
        var file = await _fileService.GetById(fileId);

        return IsAvailable(file);
    }

    public bool IsAvailable(File file)
    {
        return file is not null &&
               file.IsAttached;
    }
}