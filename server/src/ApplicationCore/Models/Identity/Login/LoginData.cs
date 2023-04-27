using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Identity.Login;

public class LoginData
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Fingerprint { get; set; }
}