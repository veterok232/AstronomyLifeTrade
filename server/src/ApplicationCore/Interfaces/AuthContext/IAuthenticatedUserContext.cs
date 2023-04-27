namespace ApplicationCore.Interfaces.AuthContext;

public interface IAuthenticatedUserContext
{
    Guid? UserId { get; }

    Guid? AssignmentId { get; }

    string RoleName { get; }

    bool IsAuthenticated { get; }

    Guid? OriginAssignmentId { get; }

    Guid? SessionId { get; }

    IEnumerable<string> Privileges { get; }

    bool IsInitialized { get; }
}