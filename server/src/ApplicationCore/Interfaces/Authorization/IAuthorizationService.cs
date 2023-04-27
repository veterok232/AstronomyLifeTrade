namespace ApplicationCore.Interfaces.Authorization;

public interface IAuthorizationService
{
    bool IsAuthorized(string privilege);
}