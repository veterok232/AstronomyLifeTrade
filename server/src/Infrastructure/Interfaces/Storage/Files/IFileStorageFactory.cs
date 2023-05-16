using ApplicationCore.Enums;

namespace Infrastructure.Interfaces.Storage.Files;

public interface IFileStorageFactory
{
    IFileStorage CreateUserStorage(FileStorageType type);

    IFileStorage CreateUserStorage();

    IFileStorage CreateSystemStorage();
}