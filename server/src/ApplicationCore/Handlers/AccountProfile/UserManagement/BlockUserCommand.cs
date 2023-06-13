using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.UserManagement;

public record BlockUserCommand(Guid UserAssignmentId) : IRequest<Result>;