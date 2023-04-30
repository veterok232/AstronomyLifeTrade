namespace ApplicationCore.Entities;

public class PersonalData : Entity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public Guid AddressId { get; set; }
    
    public Address Address { get; set; }
    
    public DateTime Birthday { get; set; }
    
    public string Email { get; set; }
    
    public string Gender { get; set; }
    
    public string Phone { get; set; }
    
    public Guid AssignmentId { get; set; }
    
    public Assignment Assignment { get; set; }
}