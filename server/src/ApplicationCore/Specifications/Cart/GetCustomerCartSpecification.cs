using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Cart;

public class GetCustomerCartSpecification : Specification<Entities.Cart>
{
    public GetCustomerCartSpecification(Guid customerAssignmentId)
        : base(c => c.CustomerAssignmentId == customerAssignmentId)
    {
        AddInclude($"{nameof(Entities.Cart.CartItems)}.{nameof(CartItem.Product)}.{nameof(Product.Brand)}");
        AddInclude($"{nameof(Entities.Cart.CartItems)}.{nameof(CartItem.Product)}.{nameof(Product.Category)}");
        AddInclude($"{nameof(Entities.Cart.CartItems)}.{nameof(CartItem.Product)}.{nameof(Product.Comments)}");
    }
}