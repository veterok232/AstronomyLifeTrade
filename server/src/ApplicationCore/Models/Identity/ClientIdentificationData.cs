using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Identity;

public class ClientIdentificationData
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Fingerprint { get; set; }
}