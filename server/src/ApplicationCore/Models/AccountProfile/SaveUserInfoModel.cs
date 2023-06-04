namespace ApplicationCore.Models.AccountProfile;

public class SaveUserInfoModel
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Gender { get; set; }
}