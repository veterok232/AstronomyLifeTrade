using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Identity.Login;
using ApplicationCore.Interfaces.Users;
using ApplicationCore.Models.Common;
using ApplicationCore.Utils;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserCreationService _userCreationService;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IUserCreationService userCreationService)
    {
        _unitOfWork = unitOfWork;
        _userCreationService = userCreationService;
    }

    public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var assignment = await _userCreationService.Create(command.Model);

        await _unitOfWork.Commit();

        return ResultBuilder.BuildSucceeded();
    }
}