using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity.Login;
using MediatR;

namespace ApplicationCore.Handlers.Identity;

public record LoginCommand(LoginData Data) : IRequest<Result>;