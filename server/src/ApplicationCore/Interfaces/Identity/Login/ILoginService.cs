using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity.Login;

namespace ApplicationCore.Interfaces.Identity.Login;

public interface ILoginService
{
    Task<Result> Login(LoginData data);
}