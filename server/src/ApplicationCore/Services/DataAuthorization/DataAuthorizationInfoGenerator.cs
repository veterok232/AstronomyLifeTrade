using System.Reflection;
using ApplicationCore.Attributes;
using ApplicationCore.Interfaces.DataAuthorization;
using ApplicationCore.Models.ModelProtection;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Utils;

namespace ApplicationCore.Services.DataAuthorization;

[ScopedDependency]
internal class DataAuthorizationInfoGenerator : IDataAuthorizationInfoGenerator
{
    private readonly IEnumerable<Type> _typesToSkip = new List<Type>
    {
        typeof(string),
        typeof(Uri),
    };

    private readonly Dictionary<Type, List<PropertyAuthorizationInfo>> _referenceTypesInProcess = new();

    public IEnumerable<PropertyAuthorizationInfo> Generate(Type typeOfModel)
    {
        var underlyingType = DataAuthorizationTypeUtils.GetUnderlyingType(typeOfModel);

        return _typesToSkip.Contains(underlyingType) ||
               underlyingType.IsValueType
            ? null
            : GenerateFromApplicableReferenceType(underlyingType);
    }

    private List<PropertyAuthorizationInfo> GenerateFromApplicableReferenceType(Type type)
    {
        if (_referenceTypesInProcess.ContainsKey(type))
        {
            return _referenceTypesInProcess[type];
        }

        var authorizationInfo = new List<PropertyAuthorizationInfo>();
        _referenceTypesInProcess.Add(type, authorizationInfo);

        foreach (var propertyInfo in type.GetProperties())
        {
            var propertyAuthorizationInfo = new PropertyAuthorizationInfo(propertyInfo)
            {
                Children = Generate(propertyInfo.PropertyType),
            };

            var requiredAttribute = propertyInfo.GetCustomAttribute<RequirePermissionAttribute>();
            if (requiredAttribute != null)
            {
                propertyAuthorizationInfo.Permission = requiredAttribute.Permission;
            }

            if (requiredAttribute != null || propertyAuthorizationInfo.Children != null)
            {
                authorizationInfo.Add(propertyAuthorizationInfo);
            }
        }

        return authorizationInfo;
    }
}
