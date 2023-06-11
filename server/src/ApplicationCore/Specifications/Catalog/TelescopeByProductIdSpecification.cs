using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class TelescopeByProductIdSpecification : Specification<Telescope>
{
    public TelescopeByProductIdSpecification(Guid productId)
        : base(t => t.ProductId == productId)
    {
        
    }
}