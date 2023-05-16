using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Identity.Login;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

internal class LoginHandler : IRequestHandler<LoginCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoginService _loginService;

    public LoginHandler(IUnitOfWork unitOfWork, ILoginService loginService)
    {
        _unitOfWork = unitOfWork;
        _loginService = loginService;
    }

    public async Task<Result> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var response = await _loginService.Login(command.Data);

        await _unitOfWork.Commit();

        return response;
    }
}