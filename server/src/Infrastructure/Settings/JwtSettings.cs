namespace Infrastructure.Settings;

public class JwtSettings
{
    public string Issuer { get; set; }

    public TimeSpan TokenLifetime { get; set; }

    public TimeSpan RefreshTokenLifetime { get; set; }

    public TimeSpan StaffRefreshTokenLifetime { get; set; }

    public SigningKeysSettings SigningKeysSettings { get; set; }
}