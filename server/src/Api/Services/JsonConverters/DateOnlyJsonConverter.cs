using System.Globalization;
using ApplicationCore.Constants;
using Newtonsoft.Json;

namespace Api.Services.JsonConverters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString(Dates.Format, CultureInfo.InvariantCulture));
    }

    public override DateOnly ReadJson(
        JsonReader reader,
        Type objectType,
        DateOnly existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = (string)reader.Value;

        return value != null ? DateOnly.Parse(value) : default;
    }
}