namespace ApplicationCore.Extensions;

public static class EnumExtensions
{
    public static string GetFullName(this Enum myEnum)
    {
        return $"{myEnum.GetType().Name}.{myEnum.ToString()}";
    }

    public static IEnumerable<T> GetAllValues<T>()
        where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static IEnumerable<T> SplitFlag<T>(this T e)
        where T : Enum
    {
        return GetAllValues<T>()
            .Where(v => e.HasFlag(v))
            .Where(v =>
            {
                var intValue = (int)(object)v;

                return (intValue & (intValue - 1)) == 0;
            });
    }
}