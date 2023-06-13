using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.AccountProfile;

namespace ApplicationCore.Services.AccountProfile;

[ScopedDependency]
public class StatisticsService : IStatisticsService
{
    private readonly IRepository<Order> _ordersRepository;

    public StatisticsService(IRepository<Order> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<List<OrdersAggregatedDataItem>> GetOrdersStatistics(StatisticsQuery query)
    {
        query.CreatedOnFrom = query.CreatedOnFrom.SetKindUtc();
        query.CreatedOnTo = query.CreatedOnTo.SetKindUtc();
        
        var orders = await _ordersRepository.List(
            new OrdersForStatisticsSpecification(query));

        return new List<OrdersAggregatedDataItem>
        {
            new OrdersAggregatedDataItem
            {
                Status = OrderStatus.Pending,
                Count = orders.Count(o => o.OrderStatus == OrderStatus.Pending),
                Amount = orders
                    .Where(o => o.OrderStatus == OrderStatus.Pending)
                    .Select(o => o.TotalAmount).Sum(),
            },
            new OrdersAggregatedDataItem
            {
                Status = OrderStatus.Postponed,
                Count = orders.Count(o => o.OrderStatus == OrderStatus.Postponed),
                Amount = orders
                    .Where(o => o.OrderStatus == OrderStatus.Postponed)
                    .Select(o => o.TotalAmount).Sum(),
            },
            new OrdersAggregatedDataItem
            {
                Status = OrderStatus.Cancelled,
                Count = orders.Count(o => o.OrderStatus == OrderStatus.Cancelled),
                Amount = orders
                    .Where(o => o.OrderStatus == OrderStatus.Cancelled)
                    .Select(o => o.TotalAmount).Sum(),
            },
            new OrdersAggregatedDataItem
            {
                Status = OrderStatus.Approved,
                Count = orders.Count(o => o.OrderStatus == OrderStatus.Approved),
                Amount = orders
                    .Where(o => o.OrderStatus == OrderStatus.Approved)
                    .Select(o => o.TotalAmount).Sum(),
            },
            new OrdersAggregatedDataItem
            {
                Status = OrderStatus.Closed,
                Count = orders.Count(o => o.OrderStatus == OrderStatus.Closed),
                Amount = orders
                    .Where(o => o.OrderStatus == OrderStatus.Closed)
                    .Select(o => o.TotalAmount).Sum(),
            },
        };
    }
}