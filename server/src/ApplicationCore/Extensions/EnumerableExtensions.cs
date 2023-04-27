namespace ApplicationCore.Extensions;

public static class EnumerableExtensions
{
    private const string Comma = ",";

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable == null || !enumerable.Any();
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source), "Source cannot be null.");
        }

        if (action == null)
        {
            return source;
        }

        foreach (var element in source)
        {
            action(element);
        }

        return source;
    }

    public static string ArrayToString(this IEnumerable<string> array, string delimiter) =>
        string.Join(delimiter, array);

    public static string ArrayToString(this IEnumerable<string> array) => array.ArrayToString(Comma);

    public static async Task<bool> All<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, Task<bool>> predicate)
    {
        foreach (TSource element in source)
        {
            if (!await predicate(element))
            {
                return false;
            }
        }

        return true;
    }

    public static async Task<bool> Any<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, Task<bool>> predicate)
    {
        foreach (var element in source)
        {
            if (await predicate(element))
            {
                return true;
            }
        }

        return false;
    }

    public static IEnumerable<TSource> EmptyIfNull<TSource>(
        this IEnumerable<TSource> source)
    {
        return source ?? Enumerable.Empty<TSource>();
    }
}