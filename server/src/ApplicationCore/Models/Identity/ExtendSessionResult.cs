namespace ApplicationCore.Models.Identity;

public class ExtendSessionResult
{
    public Guid RefreshToken { get; set; }

    public DateTime ExpiryDateTime { get; set; }
}