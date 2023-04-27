using System.Globalization;

namespace ApplicationCore.Extensions;

internal static class DictionaryExtensions
{
    public static bool GetBooleanValueByKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary != null &&
               dictionary.TryGetValue(key, out var value) &&
               Convert.ToBoolean(value, CultureInfo.InvariantCulture);
    }
}