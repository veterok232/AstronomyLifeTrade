using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

public record RegisterCommand(UserRegistrationModel Model) : IRequest<Result>;