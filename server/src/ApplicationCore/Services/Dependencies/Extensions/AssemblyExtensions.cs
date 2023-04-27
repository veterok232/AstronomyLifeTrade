using System.Reflection;

namespace ApplicationCore.Services.Dependencies.Extensions;

internal static class AssemblyExtensions
{
    public static IEnumerable<Type> GetTypesWithAttribute<T>(this Assembly assembly)
        where T : Attribute
    {
        return assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<T>() is not null);
    }
}