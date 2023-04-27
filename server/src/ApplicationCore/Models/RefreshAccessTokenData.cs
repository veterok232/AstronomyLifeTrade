namespace ApplicationCore.Models;

public class RefreshAccessTokenData
{
    public bool? IsAssignmentInactive { get; init; }

    public string Token { get; init; }

    public Guid RefreshToken { get; init; }

    public DateTime SessionExpirationDate { get; init; }
}