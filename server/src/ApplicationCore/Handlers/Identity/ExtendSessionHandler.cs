using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Identity;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

internal class ExtendSessionHandler : IRequestHandler<ExtendSessionCommand, Result<ExtendSessionResult>>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public ExtendSessionHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ExtendSessionResult>> Handle(ExtendSessionCommand command, CancellationToken cancellationToken)
    {
        var result = await _identityService.ExtendSession(command.RefreshToken, command.ClientIdentificationData);
        await _unitOfWork.Commit();

        return result;
    }
}