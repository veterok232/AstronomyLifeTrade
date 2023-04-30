namespace ApplicationCore.Entities;

public class Supplier : Entity
{
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string ProductCode { get; set; }
    
    public string ShortName { get; set; }
    
    public string AffiliateNumber { get; set; }
    
    public Guid LegalDetailsId { get; set; }
    
    public LegalDetails LegalDetails { get; set; }
}