using ApplicationCore.Enums;

namespace ApplicationCore.Interfaces.Files;

public interface IFileStorageFactory
{
    IFileStorage CreateUserStorage(FileStorageType type);

    IFileStorage CreateUserStorage();

    IFileStorage CreateSystemStorage();
}