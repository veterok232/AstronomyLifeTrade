using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class AccessoryByProductIdSpecification : Specification<Accessory>
{
    public AccessoryByProductIdSpecification(Guid productId)
        : base(a => a.ProductId == productId)
    {
        
    }
}