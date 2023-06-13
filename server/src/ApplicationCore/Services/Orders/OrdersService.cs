using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Orders;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Orders;
using ApplicationCore.Utils;

namespace ApplicationCore.Services.Orders;

[ScopedDependency]
public class OrdersService : IOrdersService
{
    private readonly IRepository<Order> _ordersRepository;
    private readonly IRepository<Product> _productsRepository;
    private readonly IOrderActionsValidator _orderActionsValidator;
    private readonly IOrderStatusValidator _orderStatusValidator;
    private readonly IAuthContextAccessor _authContextAccessor;

    public OrdersService(
        IRepository<Order> ordersRepository,
        IRepository<Product> productsRepository,
        IOrderActionsValidator orderActionsValidator,
        IOrderStatusValidator orderStatusValidator,
        IAuthContextAccessor authContextAccessor)
    {
        _ordersRepository = ordersRepository;
        _productsRepository = productsRepository;
        _orderActionsValidator = orderActionsValidator;
        _orderStatusValidator = orderStatusValidator;
        _authContextAccessor = authContextAccessor;
    }

    public async Task PostponeOrder(Guid orderId)
    {
        var order = await _ordersRepository.GetById(orderId);
        
        if (!_orderStatusValidator.IsValidForPostpone(order.OrderStatus))
        {
            throw new PotentiallyConcurrentModificationsException($"Order with id {orderId} invalid for postpone");
        }

        order.OrderStatus = OrderStatus.Postponed;
        order.ManagerAssignmentId ??= _authContextAccessor.AssignmentId;

        await _ordersRepository.Update(order);
    }

    public async Task CancelOrder(Guid orderId)
    {
        var order = await _ordersRepository.GetSingleOrDefault(
            new OrderForApproveSpecification(orderId));
        
        if (!_orderStatusValidator.IsValidForCancel(order.OrderStatus))
        {
            throw new PotentiallyConcurrentModificationsException($"Order with id {orderId} invalid for cancel");
        }

        DiscardReservationForProducts(order);
        
        order.OrderStatus = OrderStatus.Cancelled;
        order.ManagerAssignmentId ??= _authContextAccessor.AssignmentId;

        await _ordersRepository.Update(order);
    }

    public async Task<Result> ApproveOrder(Guid orderId)
    {
        var order = await _ordersRepository.GetSingleOrDefault(
            new OrderForApproveSpecification(orderId));

        if (!_orderStatusValidator.IsValidForApprove(order.OrderStatus))
        {
            throw new PotentiallyConcurrentModificationsException($"Order with id {orderId} invalid for approve");
        }

        var validationResult = _orderActionsValidator.IsValidForApprove(order);
        
        if (!validationResult.IsSucceeded)
        {
            return ResultBuilder.RebuildData(validationResult);
        }

        ReserveProducts(order);
        
        order.OrderStatus = OrderStatus.Approved;
        order.ManagerAssignmentId ??= _authContextAccessor.AssignmentId;

        await _ordersRepository.Update(order);

        return ResultBuilder.BuildSucceeded();
    }

    public async Task CloseOrder(Guid orderId)
    {
        var order = await _ordersRepository.GetSingleOrDefault(
            new OrderForApproveSpecification(orderId));
        
        if (!_orderStatusValidator.IsValidForClose(order.OrderStatus))
        {
            throw new PotentiallyConcurrentModificationsException($"Order with id {orderId} invalid for close");
        }

        MarkProductsAsSold(order);
        
        order.OrderStatus = OrderStatus.Closed;
        order.ManagerAssignmentId ??= _authContextAccessor.AssignmentId;

        await _ordersRepository.Update(order);
    }

    public async Task RemoveOrderItem(RemoveOrderItemModel model)
    {
        var order = await _ordersRepository.GetSingleOrDefault(
            new OrderForApproveSpecification(model.OrderId));

        order.OrderItems.Remove(order.OrderItems.Single(oi => oi.Id == model.OrderItemId));
        order.ManagerAssignmentId ??= _authContextAccessor.AssignmentId;
        
        await _ordersRepository.Update(order);
    }

    private void ReserveProducts(Order order)
    {
        foreach (var orderItem in order.OrderItems)
        {
            orderItem.Product.Quantity -= orderItem.Quantity;
        }
    }
    
    private void DiscardReservationForProducts(Order order)
    {
        foreach (var orderItem in order.OrderItems)
        {
            orderItem.Product.Quantity += orderItem.Quantity;
        }
    }
    
    private void MarkProductsAsSold(Order order)
    {
        foreach (var orderItem in order.OrderItems)
        {
            orderItem.Product.SoldCount += orderItem.Quantity;
        }
    }
}