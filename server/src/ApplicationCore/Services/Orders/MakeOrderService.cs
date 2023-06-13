using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Orders;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Orders;
using ApplicationCore.Utils;
using AutoMapper;

namespace ApplicationCore.Services.Orders;

[ScopedDependency]
public class MakeOrderService : IMakeOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<CartItem> _cartItemsRepository;
    private readonly IAuthContextAccessor _authContextAccessor;
    private readonly IMapper _mapper;

    public MakeOrderService(
        IRepository<Order> orderRepository,
        IAuthContextAccessor authContextAccessor,
        IMapper mapper,
        IRepository<CartItem> cartItemsRepository)
    {
        _orderRepository = orderRepository;
        _authContextAccessor = authContextAccessor;
        _mapper = mapper;
        _cartItemsRepository = cartItemsRepository;
    }

    public async Task<Result<Order>> MakeOrder(MakeOrderModel model)
    {
        var cartItems = await _cartItemsRepository.List(
            new CartItemForMakeOrderSpecification(model.CartItemsIds));
        
        return ResultBuilder.BuildSucceeded(await _orderRepository.Add(new Order
        {
            ConsumerAssignmentId = _authContextAccessor.AssignmentId.Value,
            FirstName = model.CustomerInfo.FirstName,
            LastName = model.CustomerInfo.LastName,
            PhoneNumber = model.CustomerInfo.Phone,
            Email = model.CustomerInfo.Email,
            Address = _mapper.Map<Address>(model.CustomerInfo.Address),
            OrderStatus = OrderStatus.Pending,
            TotalAmount = model.TotalAmount,
            PaymentMethod = model.PaymentMethod,
            DeliveryType = model.DeliveryType,
            CustomerNotes = model.CustomerNotes,
            ManagerAssignmentId = null,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            OrderItems = _mapper.Map<List<OrderItem>>(cartItems.ToList()),
            PromoCode = model.PromoCode,
            PromoRate = model.PromoRate,
            PromoAmount = model.PromoAmount
        }));
    }
}