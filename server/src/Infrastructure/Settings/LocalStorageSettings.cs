namespace Infrastructure.Settings;

public class LocalStorageSettings
{
    public FileSystemStorageSettings UserStorage { get; init; }

    public FileSystemStorageSettings SystemStorage { get; init; }
}