namespace ApplicationCore.Entities;

public class LegalDetails : Entity
{
    public string BankName { get; set; }
    
    public string Bic { get; set; }
    
    public string Iban { get; set; }
    
    public Guid LegalAddressId { get; set; }
    
    public Address LegalAddress { get; set; }
    
    public string LegalName { get; set; }
    
    public string Unp { get; set; }
}