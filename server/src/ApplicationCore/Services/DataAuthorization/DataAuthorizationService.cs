using System.Collections.Concurrent;
using ApplicationCore.Interfaces.DataAuthorization;
using ApplicationCore.Models.ModelProtection;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.DataAuthorization;

[ScopedDependency]
internal class DataAuthorizationService : IDataAuthorizationService
{
    private readonly ConcurrentDictionary<Type, IEnumerable<PropertyAuthorizationInfo>> _cache = new();
    private readonly IDataAuthorizationInfoGenerator _dataAuthorizationInfoGenerator;
    private readonly IDataAuthorizationProtector _protector;

    public DataAuthorizationService(
        IDataAuthorizationInfoGenerator dataAuthorizationInfoGenerator,
        IDataAuthorizationProtector protector)
    {
        _dataAuthorizationInfoGenerator = dataAuthorizationInfoGenerator;
        _protector = protector;
    }

    public void Authorize(object obj)
    {
        var objType = obj.GetType();
        if (!_cache.TryGetValue(objType, out var authorizationInfo))
        {
            authorizationInfo = _dataAuthorizationInfoGenerator.Generate(objType);
            _cache.TryAdd(objType, authorizationInfo);
        }

        if (authorizationInfo is not null)
        {
            _protector.RemoveNotAuthorizedData(obj, authorizationInfo);
        }
    }
}