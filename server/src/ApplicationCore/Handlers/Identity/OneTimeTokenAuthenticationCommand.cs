using System.Security.Claims;
using ApplicationCore.Enums;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

public class OneTimeTokenAuthenticationCommand : IRequest<Result<ClaimsPrincipal>>
{
    public Guid Token { get; set; }

    public OneTimeTokenTermType TokenTermType { get; set; }
}