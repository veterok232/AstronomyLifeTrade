using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Cart;

public class CustomerCartSpecification : Specification<Entities.Cart>
{
    public CustomerCartSpecification(Guid customerAssignmentId)
        : base(c => c.CustomerAssignmentId == customerAssignmentId)
    {
        AddInclude($"{nameof(Entities.Cart.CartItems)}.{nameof(CartItem.Product)}");
    }
}