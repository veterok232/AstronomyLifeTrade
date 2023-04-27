using System.Collections;

namespace ApplicationCore.Utils;

internal static class DataAuthorizationTypeUtils
{
    public static Type GetUnderlyingType(Type type)
    {
        if (type.IsArray)
        {
            return type.GetElementType();
        }

        // Check whether type is string, because string implements IEnumerable
        if (!type.IsAssignableTo(typeof(IEnumerable)) || type == typeof(string))
        {
            return type;
        }

        var genericArguments = type.GetGenericArguments();

        // If type is generic it must have one or two generic arguments.
        // For generic list should be one argument.
        // For generic dictionary should be two arguments:
        // First - type of key
        // Second(Last) - type of value
        return GetUnderlyingType(genericArguments.Length > 1
            ? genericArguments.Last() :
            genericArguments.Single());
    }
}