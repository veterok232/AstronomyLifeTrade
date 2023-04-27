namespace ApplicationCore.Settings;

public class TemporaryFilesDeleteSettings
{
    public string JobCronExpression { get; set; }

    public int ExpirationTimeInDays { get; set; }
}