using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.UserManagement;

internal class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, Result>
{
    private readonly IUserManagementService _userManagementService;
    private readonly IUnitOfWork _unitOfWork;

    public UnblockUserCommandHandler(
        IUserManagementService userManagementService,
        IUnitOfWork unitOfWork)
    {
        _userManagementService = userManagementService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UnblockUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _userManagementService.UnblockUser(command.UserAssignmentId);

        await _unitOfWork.Commit();

        return result;
    }
}