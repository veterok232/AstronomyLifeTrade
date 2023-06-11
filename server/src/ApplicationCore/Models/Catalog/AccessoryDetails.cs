using ApplicationCore.Enums;

namespace ApplicationCore.Models.Catalog;

public class AccessoryDetails : ProductDetails
{
    public AccessoryType? AccessoryType { get; set; }
}