namespace Infrastructure.Settings;

public class OneTimeTokenAuthenticationSettings
{
    public IEnumerable<OneTimeTokenSettings> TokensSettings { get; set; }
}