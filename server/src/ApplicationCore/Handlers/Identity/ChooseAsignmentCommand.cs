using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

public class ChooseAssignmentCommand : IRequest<Result>
{
    public Guid RefreshToken { get; set; }

    public ChangeAssignmentData Data { get; set; }
}