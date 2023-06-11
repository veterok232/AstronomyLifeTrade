using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class BinocularByProductIdSpecification : Specification<Binocular>
{
    public BinocularByProductIdSpecification(Guid productId)
        : base(b => b.ProductId == productId)
    {
        
    }
}