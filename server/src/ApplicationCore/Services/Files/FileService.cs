using ApplicationCore.Constants;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Files;
using ApplicationCore.Services.Dependencies.Attributes;
using File = ApplicationCore.Entities.File;

namespace ApplicationCore.Services.Files;

[ScopedDependency]
internal class FileService : IFileService
{
    private readonly IRepository<File> _repository;
    private readonly IAuthContextAccessor _authContextAccessor;

    public FileService(IRepository<File> repository, IAuthContextAccessor authContextAccessor)
    {
        _repository = repository;
        _authContextAccessor = authContextAccessor;
    }

    public Task<File> Create(File file)
    {
        return _repository.Add(file);
    }

    public Task<File> GetById(Guid fileId)
    {
        return _repository.GetById(fileId);
    }

    public Task<File> Update(File file)
    {
        return _repository.Update(file);
    }

    public Task Update(IEnumerable<File> files)
    {
        return _repository.Update(files);
    }

    public Task Delete(File file) => _repository.Delete(file);
}