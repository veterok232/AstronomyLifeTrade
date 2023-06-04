using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Cart;
using ApplicationCore.Models.Cart;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Cart;
using AutoMapper;

namespace ApplicationCore.Services.Cart;

[ScopedDependency]
public class CartService : ICartService
{
    private readonly IRepository<Entities.Cart> _cartRepository;
    private readonly IAuthContextAccessor _authContextAccessor;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IMapper _mapper;

    public CartService(
        IRepository<Entities.Cart> cartRepository,
        IAuthContextAccessor authContextAccessor,
        IRepository<Product> productRepository,
        IRepository<CartItem> cartItemRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _authContextAccessor = authContextAccessor;
        _productRepository = productRepository;
        _cartItemRepository = cartItemRepository;
        _mapper = mapper;
    }

    public async Task<CartModel> Get()
    {
        var cart = await _cartRepository.GetSingleOrDefault(
            new GetCustomerCartSpecification(_authContextAccessor.AssignmentId.Value));

        return _mapper.Map<CartModel>(cart);
    }

    public async Task<ICollection<Guid>> GetProductsInCart()
    {
        var cart = await _cartRepository.GetSingleOrDefault(
            new GetCustomerCartSpecification(_authContextAccessor.AssignmentId.Value));

        return cart.CartItems.Select(ci => ci.ProductId).ToList();
    }

    public async Task Add(Guid productId)
    {
        var customerCart = await _cartRepository.GetSingleOrDefault(
            new CustomerCartSpecification(_authContextAccessor.AssignmentId.Value));

        var product = await _productRepository.GetById(productId);

        if (customerCart == null)
        {
            await _cartRepository.Add(new Entities.Cart
            {
                CustomerAssignmentId = _authContextAccessor.AssignmentId.Value,
                TotalAmount = product.Price,
                Quantity = 1,
                CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = productId,
                        Quantity = 1
                    }
                }
            });
        }
        else
        {
            customerCart.Quantity++;
            customerCart.TotalAmount += product.Price;

            var cartItem = customerCart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                customerCart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = 1
                });
            }
            
            await _cartRepository.Update(customerCart);
        }
    }

    public async Task Remove(Guid productId)
    {
        var customerCart = await _cartRepository.GetSingleOrDefault(
            new CustomerCartSpecification(_authContextAccessor.AssignmentId.Value));

        var cartItem = customerCart.CartItems.First(ci => ci.ProductId == productId);

        customerCart.Quantity -= cartItem.Quantity;
        customerCart.TotalAmount -= cartItem.Product.Price * cartItem.Quantity;
        customerCart.CartItems.Remove(cartItem);

        await _cartRepository.Update(customerCart);
        await _cartItemRepository.Delete(cartItem);
    }

    public async Task ChangeProductCount(CartChangeCountModel model)
    {
        var customerCart = await _cartRepository.GetSingleOrDefault(
            new CustomerCartSpecification(_authContextAccessor.AssignmentId.Value));
        
        var cartItem = customerCart.CartItems.First(ci => ci.ProductId == model.ProductId);

        customerCart.Quantity += model.Count - cartItem.Quantity;
        customerCart.TotalAmount = cartItem.Product.Price * model.Count;
        cartItem.Quantity = model.Count;

        await _cartRepository.Update(customerCart);
    }

    public async Task Clear()
    {
        var customerCart = await _cartRepository.GetSingleOrDefault(
            new CustomerCartSpecification(_authContextAccessor.AssignmentId.Value));
        
        customerCart.CartItems.Clear();
        customerCart.Quantity = 0;
        customerCart.TotalAmount = 0;
        
        await _cartRepository.Update(customerCart);
    }
}