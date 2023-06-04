namespace ApplicationCore.Entities;

public class Address : Entity
{
    public string? Building { get; set; }
    
    public string City { get; set; }
    
    public string? Country { get; set; }
    
    public string? Flat { get; set; }
    
    public string PostalCode { get; set; }
    
    public string? Street { get; set; }
    
    public string FullAddress { get; set; }
}