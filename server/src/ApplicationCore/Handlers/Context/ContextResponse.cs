namespace ApplicationCore.Handlers.Context;

public class ContextResponse
{
    public bool IsAuthenticated { get; set; }

    public string Lang { get; set; }

    public Guid? UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string RoleName { get; set; }

    public DateTime? RefreshTokenExpirationDateTime { get; set; }
}