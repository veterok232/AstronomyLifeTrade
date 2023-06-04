namespace ApplicationCore.Models.AccountProfile;

public class UserInfoModel
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Phone { get; set; }
    
    public string Email { get; set; }
    
    public AddressModel? Address { get; set; }
    
    public DateTime? Birthday { get; set; }
    
    public string? Gender { get; set; }
}