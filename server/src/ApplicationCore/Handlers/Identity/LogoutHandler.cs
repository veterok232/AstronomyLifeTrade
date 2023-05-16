using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Identity;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

internal class LogoutHandler : IRequestHandler<LogoutCommand, Unit>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutHandler(
        IIdentityService identityService,
        IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        await _identityService.Logout();
        await _unitOfWork.Commit();

        return Unit.Value;
    }
}