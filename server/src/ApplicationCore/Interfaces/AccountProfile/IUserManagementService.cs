using ApplicationCore.Models.Common;

namespace ApplicationCore.Interfaces.AccountProfile;

public interface IUserManagementService
{
    Task<Result> AssignAsManager(Guid userAssignmentId);
    
    Task<Result> AssignAsAdministrator(Guid userAssignmentId);
    
    Task<Result> BlockUser(Guid userAssignmentId);
    
    Task<Result> UnblockUser(Guid userAssignmentId);
}