using ApplicationCore.Models.Catalog;

namespace ApplicationCore.Interfaces.Catalog.Search;

public interface IFillCharacteristicsService
{
    Task FillCharacteristics(ICollection<ProductListItem> products);
}