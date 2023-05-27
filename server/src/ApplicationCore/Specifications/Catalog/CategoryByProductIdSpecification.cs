using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Models.Catalog;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Catalog;

public class ProductByIdForDetailsSpecification : Specification<Product>
{
    public ProductByIdForDetailsSpecification(Guid productId)
        : base(p => p.Id == productId)
    {
        AddIncludes(
            p => p.Category,
            p => p.Files);
    }
}