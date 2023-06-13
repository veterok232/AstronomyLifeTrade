using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.UserManagement;

internal class AssignAsManagerCommandHandler : IRequestHandler<AssignAsManagerCommand, Result>
{
    private readonly IUserManagementService _userManagementService;
    private readonly IUnitOfWork _unitOfWork;

    public AssignAsManagerCommandHandler(
        IUserManagementService userManagementService,
        IUnitOfWork unitOfWork)
    {
        _userManagementService = userManagementService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        AssignAsManagerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _userManagementService.AssignAsManager(command.UserAssignmentId);

        await _unitOfWork.Commit();

        return result;
    }
}