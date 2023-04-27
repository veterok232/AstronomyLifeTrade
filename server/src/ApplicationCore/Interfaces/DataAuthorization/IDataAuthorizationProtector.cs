using ApplicationCore.Models.ModelProtection;

namespace ApplicationCore.Interfaces.DataAuthorization;

internal interface IDataAuthorizationProtector
{
    void RemoveNotAuthorizedData(object model, IEnumerable<PropertyAuthorizationInfo> propertiesAuthorizationInfo);
}