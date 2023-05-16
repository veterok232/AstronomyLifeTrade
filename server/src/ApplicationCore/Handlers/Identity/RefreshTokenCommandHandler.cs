using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Identity;
using ApplicationCore.Models;
using ApplicationCore.Models.Common;
using ApplicationCore.Utils;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<RefreshAccessTokenData>>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(
        IIdentityService identityService,
        IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RefreshAccessTokenData>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        if (command.RefreshToken == null)
        {
            return ResultBuilder.BuildFailed<RefreshAccessTokenData>();
        }

        var result = await _identityService.RefreshAccessToken(
            command.RefreshToken.Value, command.ClientIdentificationData);
        await _unitOfWork.Commit();

        return result;
    }
}