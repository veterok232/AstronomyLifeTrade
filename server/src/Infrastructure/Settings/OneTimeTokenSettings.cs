using ApplicationCore.Enums;

namespace Infrastructure.Settings;

public class OneTimeTokenSettings
{
    public OneTimeTokenTermType TokenTermType { get; set; }

    public TimeSpan TokenLifetime { get; set; }
}