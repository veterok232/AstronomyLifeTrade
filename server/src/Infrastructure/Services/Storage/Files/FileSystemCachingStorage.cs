using System.Collections;
using ApplicationCore.Interfaces.Files;
using Infrastructure.Interfaces;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Storage.Files;

internal class FileSystemCachingStorage : IFileStorage
{
    private readonly IFileStorage _origin;
    private readonly ISequentialRunnerFactory _sequentialRunnerFactory;
    private readonly string _rootPath;

    private static readonly Hashtable CachedPaths = new Hashtable();

    public FileSystemCachingStorage(
        IFileStorage origin,
        IOptions<FileStorageCacheSettings> settings,
        ISequentialRunnerFactory sequentialRunnerFactory)
    {
        _origin = origin;
        _sequentialRunnerFactory = sequentialRunnerFactory;
        _rootPath = settings.Value.RootPath;
    }

    public Task Save(string fileReference, Stream stream)
    {
        return _origin.Save(fileReference, stream);
    }

    public async Task<Stream> Get(string fileReference)
    {
        var path = GetFilePath(fileReference);
        if (!IsFileCached(path))
        {
            await using var originFile = await _origin.Get(fileReference);
            await SaveToCacheConcurrent(originFile, path);
        }

        return File.OpenRead(path);
    }

    private async Task SaveToCacheConcurrent(Stream originFile, string path)
    {
        var runner = await _sequentialRunnerFactory.Create<FileSystemCachingStorage>();
        await runner.Run(() => SaveToCache(originFile, path));
    }

    private static async Task SaveToCache(Stream originFile, string path)
    {
        if (IsFileCached(path))
        {
            return;
        }

        await using var cachedFile = File.Create(path);
        await originFile.CopyToAsync(cachedFile);
        CachedPaths.Add(path, null);
    }

    public async Task Delete(string fileReference)
    {
        var runner = await _sequentialRunnerFactory.Create<FileSystemCachingStorage>();
        await runner.Run(() => DeleteFromCache(fileReference));
        await _origin.Delete(fileReference);
    }

    private void DeleteFromCache(string fileReference)
    {
        var path = GetFilePath(fileReference);
        if (IsFileCached(path))
        {
            CachedPaths.Remove(path);
            File.Delete(path);
        }
    }

    private string GetFilePath(string fileReference) => Path.Combine(_rootPath, fileReference);

    private static bool IsFileCached(string path)
    {
        return CachedPaths.ContainsKey(path) && File.Exists(path);
    }
}