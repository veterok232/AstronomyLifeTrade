using ApplicationCore.Enums;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Settings;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Storage.Files;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Storage.Files;

[ScopedDependency]
internal class FileStorageFactory : IFileStorageFactory
{
    private readonly Lazy<IOptions<FileStorageSettings>> _storageOptions;
    private readonly Lazy<IOptions<LocalStorageSettings>> _localOptions;
    private readonly Lazy<IOptions<FileStorageCacheSettings>> _cacheSettings;
    private readonly Lazy<ISequentialRunnerFactory> _sequentialRunnerFactory;

    public FileStorageFactory(
        Lazy<IOptions<FileStorageCacheSettings>> cacheSettings,
        Lazy<ISequentialRunnerFactory> sequentialRunnerFactory,
        Lazy<IOptions<LocalStorageSettings>> localOptions,
        Lazy<IOptions<FileStorageSettings>> storageOptions)
    {
        _cacheSettings = cacheSettings;
        _sequentialRunnerFactory = sequentialRunnerFactory;
        _localOptions = localOptions;
        _storageOptions = storageOptions;
    }

    public IFileStorage CreateUserStorage(FileStorageType type)
    {
        return type switch
        {
            FileStorageType.FileSystem => new FileSystemStorage(_localOptions.Value.Value.UserStorage),
            _ => throw new InvalidOperationException($"No storage implementation found for '{type}' file type."),
        };
    }

    public IFileStorage CreateUserStorage()
    {
        return CreateUserStorage(_storageOptions.Value.Value.StorageType);
    }

    public IFileStorage CreateSystemStorage()
    {
        var type = _storageOptions.Value.Value.StorageType;
        IFileStorage storage = type switch
        {
            FileStorageType.FileSystem => new FileSystemStorage(_localOptions.Value.Value.SystemStorage),
            _ => throw new InvalidOperationException($"No storage implementation found for '{type}' file type."),
        };

        return new FileSystemCachingStorage(storage, _cacheSettings.Value, _sequentialRunnerFactory.Value);
    }
}