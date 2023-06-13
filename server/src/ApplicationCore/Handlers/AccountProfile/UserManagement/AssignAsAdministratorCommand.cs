using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.UserManagement;

public record AssignAsAdministratorCommand(Guid UserAssignmentId) : IRequest<Result>;