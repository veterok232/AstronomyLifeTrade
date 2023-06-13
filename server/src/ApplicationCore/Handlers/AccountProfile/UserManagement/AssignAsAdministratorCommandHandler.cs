using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.UserManagement;

internal class AssignAsAdministratorCommandHandler : IRequestHandler<AssignAsAdministratorCommand, Result>
{
    private readonly IUserManagementService _userManagementService;
    private readonly IUnitOfWork _unitOfWork;

    public AssignAsAdministratorCommandHandler(
        IUserManagementService userManagementService,
        IUnitOfWork unitOfWork)
    {
        _userManagementService = userManagementService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        AssignAsAdministratorCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _userManagementService.AssignAsAdministrator(command.UserAssignmentId);

        await _unitOfWork.Commit();

        return result;
    }
}