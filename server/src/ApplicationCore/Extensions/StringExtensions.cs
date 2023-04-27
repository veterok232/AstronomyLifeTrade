using System.Globalization;
using System.Text;

namespace ApplicationCore.Extensions;

public static class StringExtensions
{
    private const string Comma = ",";

    public static IEnumerable<string> StringToArray(this string value, string delimiter = Comma)
    {
        return (value ?? string.Empty).Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
    }

    public static IEnumerable<string> StringToArrayByCommaDelimiter(this string value)
    {
        return StringToArray(value);
    }

    public static string Reverse(this string value)
    {
        return new string(Enumerable.Reverse(value).ToArray());
    }

    public static string CamelToPascalCase(this string value)
    {
        return value.IsNullOrEmpty()
            ? value
            : char.ToUpper(value.First(), CultureInfo.InvariantCulture) + value.Substring(1);
    }

    public static byte[] ToUtf8ByteArray(this string value) => Encoding.UTF8.GetBytes(value);

    public static MemoryStream ToMemoryStreamFromBase64(this string source)
    {
        var bytes = Convert.FromBase64String(source);
        var memoryStream = new MemoryStream(bytes);

        return memoryStream;
    }

    public static string ToSentenceCase(this string value)
    {
        return value.IsNullOrEmpty()
            ? value
            : char.ToUpper(value.First(), CultureInfo.InvariantCulture) + value.Substring(1).ToLower(CultureInfo.InvariantCulture);
    }

    public static string PascalToCamelCase(this string value)
    {
        return value.IsNullOrEmpty()
            ? value
            : char.ToLowerInvariant(value.First()) + value.Substring(1);
    }

    public static bool IsNotNullOrEmpty(this string value)
    {
        return !value.IsNullOrEmpty();
    }

    public static int ToInt(this string value)
    {
        return int.Parse(value, CultureInfo.InvariantCulture);
    }

    public static bool EqualsOrdinalIgnoreCase(this string value, string comparableString)
    {
        return value.Equals(comparableString, StringComparison.OrdinalIgnoreCase);
    }
}