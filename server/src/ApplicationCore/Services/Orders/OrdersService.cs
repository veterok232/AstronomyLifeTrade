using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Services.Dependencies.Attributes;

namespace ApplicationCore.Services.Orders;

[ScopedDependency]
public class OrdersService : IOrdersService
{
    private readonly IRepository<Order> _ordersRepository;

    public OrdersService(IRepository<Order> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task PostponeOrder(Guid orderId)
    {
        var order = await _ordersRepository.GetById(orderId);

        order.OrderStatus = OrderStatus.Postponed;

        await _ordersRepository.Update(order);
    }

    public async Task CancelOrder(Guid orderId)
    {
        var order = await _ordersRepository.GetById(orderId);

        order.OrderStatus = OrderStatus.Cancelled;

        await _ordersRepository.Update(order);
    }

    public async Task ApproveOrder(Guid orderId)
    {
        var order = await _ordersRepository.GetById(orderId);

        order.OrderStatus = OrderStatus.Approved;

        await _ordersRepository.Update(order);
    }

    public async Task CloseOrder(Guid orderId)
    {
        var order = await _ordersRepository.GetById(orderId);

        order.OrderStatus = OrderStatus.Closed;

        await _ordersRepository.Update(order);
    }
}