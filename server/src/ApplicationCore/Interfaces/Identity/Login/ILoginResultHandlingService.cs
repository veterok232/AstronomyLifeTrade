using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity.Login;

namespace ApplicationCore.Services.Identity.Login;

public interface ILoginResultHandlingService
{
    Task<Result> Handle(LoginResult result);
}