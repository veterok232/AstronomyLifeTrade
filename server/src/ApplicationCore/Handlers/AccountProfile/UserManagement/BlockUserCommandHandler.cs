using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.UserManagement;

internal class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, Result>
{
    private readonly IUserManagementService _userManagementService;
    private readonly IUnitOfWork _unitOfWork;

    public BlockUserCommandHandler(
        IUserManagementService userManagementService,
        IUnitOfWork unitOfWork)
    {
        _userManagementService = userManagementService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        BlockUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _userManagementService.BlockUser(command.UserAssignmentId);

        await _unitOfWork.Commit();

        return result;
    }
}