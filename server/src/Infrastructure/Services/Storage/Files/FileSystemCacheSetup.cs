using ApplicationCore.Extensions;
using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Storage.Files;

[SelfScopedDependency]
public class FileSystemCacheSetup
{
    private readonly string _rootPath;

    public FileSystemCacheSetup(IOptions<FileStorageCacheSettings> settings)
    {
        _rootPath = settings.Value.RootPath;
    }

    public void Run()
    {
        if (!Directory.Exists(_rootPath))
        {
            Directory.CreateDirectory(_rootPath);
        }

        Directory.GetFiles(_rootPath).ForEach(File.Delete);
    }
}