namespace ApplicationCore.Interfaces.Files;

public interface IFileStorage
{
    Task Save(string fileReference, Stream stream);

    Task<Stream> Get(string fileReference);

    Task Delete(string fileReference);
}