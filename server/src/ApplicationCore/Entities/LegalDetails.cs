namespace ApplicationCore.Entities;

public class LegalDetails : Entity
{
    public string BankName { get; set; }
    
    public string Bic { get; set; }
    
    public string Iban { get; set; }
    
    public string LegalAddress { get; set; }
    
    public string LegalName { get; set; }
    
    public string Unp { get; set; }
}