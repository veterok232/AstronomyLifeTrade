using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

public class ExtendSessionCommand : IRequest<Result<ExtendSessionResult>>
{
    public Guid RefreshToken { get; set; }

    public ClientIdentificationData ClientIdentificationData { get; set; }
}