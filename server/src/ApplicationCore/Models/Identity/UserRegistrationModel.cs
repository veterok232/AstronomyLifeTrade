using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Identity;

public class UserRegistrationModel
{
    public string Email { get; set; }

    public string Password { get; set; }
    
    public string RetypedPassword { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string Phone { get; set; }
}