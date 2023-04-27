namespace ApplicationCore.Models.Identity;

public class IdentityData
{
    public string Token { get; set; }

    public Guid UserId { get; set; }

    public Guid RefreshToken { get; set; }

    public DateTime SessionExpirationDate { get; set; }
}