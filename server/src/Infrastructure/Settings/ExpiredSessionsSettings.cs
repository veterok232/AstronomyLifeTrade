namespace Infrastructure.Settings;

public class ExpiredSessionsSettings
{
    public int KeepExpiredInDays { get; set; }

    public string CleanupJobCronExpression { get; set; }
}