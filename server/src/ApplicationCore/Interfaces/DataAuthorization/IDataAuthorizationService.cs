namespace ApplicationCore.Interfaces.DataAuthorization;

public interface IDataAuthorizationService
{
    void Authorize(object obj);
}