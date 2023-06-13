using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.UserManagement;

public record UnblockUserCommand(Guid UserAssignmentId) : IRequest<Result>;