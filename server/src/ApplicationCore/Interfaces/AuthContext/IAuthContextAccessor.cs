namespace ApplicationCore.Interfaces.AuthContext;

public interface IAuthContextAccessor
{
    /// <summary>
    ///     Gets id of user that was triggered current action.
    /// </summary>
    Guid? UserId { get; }

    /// <summary>
    ///     Gets id of current assignment of user that was triggered current action.
    /// </summary>
    Guid? AssignmentId { get; }

    /// <summary>
    ///     Gets name of current user's role.
    /// </summary>
    string RoleName { get; }

    /// <summary>
    ///     Gets a value indicating whether current user is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    ///     Gets a value indicating identifier of an assignment of the user who originated "proxy" access.
    /// </summary>
    Guid? OriginAssignmentId { get; }

    Guid? SessionId { get; }

    IEnumerable<string> Privileges { get; }

    void SetContext(IAuthenticatedUserContext context);
}