namespace ApplicationCore.Settings;

public record AppInfoSettings
{
    public string Version { get; init; }

    public string Environment { get; init; }
}