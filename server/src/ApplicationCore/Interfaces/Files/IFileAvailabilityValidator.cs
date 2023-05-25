using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Interfaces.Files;

internal interface IFileAvailabilityValidator
{
    Task<bool> IsAvailable(Guid fileId);

    bool IsAvailable(File file);
}