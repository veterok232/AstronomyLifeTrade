using ApplicationCore.Extensions;

namespace Api.Settings;

public record CorsSettings
{
    public string AvailableOrigins { get; init; }

    public IEnumerable<string> SplitAvailableOrigins()
    {
        return AvailableOrigins?.StringToArray(delimiter: ";");
    }
}