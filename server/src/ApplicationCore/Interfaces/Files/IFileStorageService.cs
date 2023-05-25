using ApplicationCore.Models.Files;
using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Interfaces.Files;

public interface IFileStorageService
{
    Task<File> SaveWithFileCreation(File file, Stream stream);

    Task<File> Save(File file, Stream stream);

    Task<FileDescriptor> GetOwned(Guid fileId);

    Task<FileDescriptor> Get(Guid fileId);

    Task<FileDescriptor> Get(File file);

    Task Delete(File file);
}