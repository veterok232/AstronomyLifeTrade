namespace ApplicationCore.Services.Dependencies.Extensions;

internal static class ObjectExtensions
{
    public static T As<T>(this object o)
        where T : class
    {
        return o as T;
    }
}