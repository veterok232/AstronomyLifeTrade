using ApplicationCore.Services.Dependencies.Attributes;
using Infrastructure.Interfaces.Storage.Files;
using Infrastructure.Settings;

namespace Infrastructure.Services.Storage.Files;

[ScopedDependency]
internal class FileSystemStorage : IFileStorage
{
    private readonly FileSystemStorageSettings _settings;

    public FileSystemStorage(FileSystemStorageSettings settings)
    {
        _settings = settings;
    }

    public async Task Save(string fileReference, Stream stream)
    {
        await using var fileStream = File.Create(GetFilePath(fileReference));
        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(fileStream);
    }

    /// <param name="fileReference">In this implementation fileReference is the relative path to the file.</param>
    public async Task<Stream> Get(string fileReference)
    {
        var path = GetFilePath(fileReference);
        VerifyFileExistence(path);

        return new MemoryStream(await File.ReadAllBytesAsync(path));
    }

    public Task Delete(string fileReference)
    {
        var path = GetFilePath(fileReference);
        File.Delete(path);

        return Task.CompletedTask;
    }

    private static void VerifyFileExistence(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Cannot find the file.", path);
        }
    }

    private string GetFilePath(string fileReference) => Path.Combine(_settings.RootPath, fileReference);
}