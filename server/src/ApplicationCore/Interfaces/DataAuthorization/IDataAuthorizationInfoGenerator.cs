using ApplicationCore.Models.ModelProtection;

namespace ApplicationCore.Interfaces.DataAuthorization;

internal interface IDataAuthorizationInfoGenerator
{
    IEnumerable<PropertyAuthorizationInfo> Generate(Type typeOfModel);
}