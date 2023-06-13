using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.UserManagement;

public record AssignAsManagerCommand(Guid UserAssignmentId) : IRequest<Result>;