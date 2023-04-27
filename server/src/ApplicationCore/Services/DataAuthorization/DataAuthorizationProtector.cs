using System.Collections;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Authorization;
using ApplicationCore.Interfaces.DataAuthorization;
using ApplicationCore.Models.ModelProtection;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.DataAuthorization;

[ScopedDependency]
internal class DataAuthorizationProtector : IDataAuthorizationProtector
{
    private readonly IAuthorizationService _authorizationService;

    public DataAuthorizationProtector(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public void RemoveNotAuthorizedData(object model, IEnumerable<PropertyAuthorizationInfo> propertiesAuthorizationInfo)
    {
        if (model is IEnumerable enumerable)
        {
            RemoveDataFromEnumerable(enumerable, propertiesAuthorizationInfo);
            return;
        }

        foreach (var propertyAuthorizationInfo in propertiesAuthorizationInfo)
        {
            RemoveDataFromProperty(model, propertyAuthorizationInfo);
        }
    }

    private void RemoveDataFromEnumerable(IEnumerable enumerable, IEnumerable<PropertyAuthorizationInfo> authorizationInfo)
    {
        if (enumerable is IDictionary dictionary)
        {
            enumerable = dictionary.Values;
        }

        foreach (var model in enumerable)
        {
            RemoveNotAuthorizedData(model, authorizationInfo);
        }
    }

    private void RemoveDataFromProperty(object property, PropertyAuthorizationInfo authorizationInfo)
    {
        var value = authorizationInfo.PropertyInfo.GetValue(property);
        if (value == null)
        {
            return;
        }

        if (!authorizationInfo.Children.IsNullOrEmpty())
        {
            RemoveNotAuthorizedData(value, authorizationInfo.Children);
        }

        if (!authorizationInfo.Permission.IsNullOrEmpty() &&
            !_authorizationService.IsAuthorized(authorizationInfo.Permission))
        {
            authorizationInfo.PropertyInfo.SetValue(property, value: default);
        }
    }
}