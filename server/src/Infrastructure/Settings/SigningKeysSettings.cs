namespace Infrastructure.Settings;

public class SigningKeysSettings
{
    public bool EnableRotation { get; set; }

    public string LocalKey { get; set; }

    public TimeSpan KeysSyncInterval { get; set; }

    public int KeySignLifetimeInDays { get; set; }

    public int KeyValidationLifetimeInDays { get; set; }

    public int KeyPreliminaryCreationInDays { get; set; }

    public string KeyCreationCronExpression { get; set; }

    public string KeysRemovingCronExpression { get; set; }
}