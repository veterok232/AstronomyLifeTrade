using ApplicationCore.Enums;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

public class CreateOneTimeTokenCommand : IRequest<Guid>
{
    public OneTimeTokenTermType TokenTermType { get; set; }
}