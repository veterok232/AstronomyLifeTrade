namespace ApplicationCore.Entities;

public class CurrencyExchange : Entity
{
    public Guid FromCurrencyId { get; set; }
    
    public Currency FromCurrency { get; set; }
    
    public Guid ToCurrencyId { get; set; }
    
    public Currency ToCurrency { get; set; }
    
    public float Ratio { get; set; }
}