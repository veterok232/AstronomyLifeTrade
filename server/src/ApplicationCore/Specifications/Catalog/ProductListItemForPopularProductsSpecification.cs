using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

internal class ProductListItemForPopularProductsSpecification : Specification<SoldProduct>
{
    public ProductListItemForPopularProductsSpecification()
        : base(sp => sp.CreatedAt > DateTime.UtcNow.AddDays(-30))
    {
        AddInclude(sp => sp.Product);
        
        ApplyPaging(1, 6);
        ApplyOrderByDescending(sp => sp.Quantity);
    }
    
    
}