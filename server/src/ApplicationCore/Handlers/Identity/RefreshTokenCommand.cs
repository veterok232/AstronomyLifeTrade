using ApplicationCore.Models;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

public class RefreshTokenCommand : IRequest<Result<RefreshAccessTokenData>>
{
    public Guid? RefreshToken { get; set; }

    public ClientIdentificationData ClientIdentificationData { get; set; }
}