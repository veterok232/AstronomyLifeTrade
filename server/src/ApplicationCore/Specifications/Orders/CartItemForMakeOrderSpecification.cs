using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Orders;

public class CartItemForMakeOrderSpecification : Specification<CartItem>
{
    public CartItemForMakeOrderSpecification(ICollection<Guid> cartItemsIds)
        : base(ci => cartItemsIds.Contains(ci.Id))
    {
        AddIncludes(
            ci => ci.Product);
    }
}