using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Interfaces.Files;

public interface IFileService
{
    Task<File> Create(File file);

    Task<File> GetById(Guid fileId);

    Task<File> Update(File file);

    Task Update(IEnumerable<File> files);

    Task Delete(File file);
}