using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class ProductForEditSpecification : Specification<Product>
{
    public ProductForEditSpecification(Guid productId)
        : base(p => p.Id == productId)
    {
        
    }
}