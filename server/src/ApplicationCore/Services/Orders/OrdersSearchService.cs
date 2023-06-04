using ApplicationCore.Entities;
using ApplicationCore.Handlers.Common;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Interfaces.Search;
using ApplicationCore.Models.Orders;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Orders;
using AutoMapper;

namespace ApplicationCore.Services.Orders;

[ScopedDependency]
public class OrdersSearchService : IOrdersSearchService
{
    private readonly IRepository<Order> _ordersRepository;
    private readonly IMapper _mapper;
    private readonly IEntityFilterQueryBuilder<Order, OrdersSearchModel> _entityFilterQueryBuilder;

    public OrdersSearchService(
        IRepository<Order> ordersRepository,
        IMapper mapper,
        IEntityFilterQueryBuilder<Order, OrdersSearchModel> entityFilterQueryBuilder)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
        _entityFilterQueryBuilder = entityFilterQueryBuilder;
    }

    public async Task<SearchResult<OrderListItem>> Search(OrdersSearchModel searchModel)
    {
        var list = await _ordersRepository.Search(
            new OrdersListSpecification(
                GetOrdersSearchData(searchModel)));
        
        var result = _mapper.Map<SearchResult<OrderListItem>>(list);
        
        return result;
    }

    public async Task<OrderDetailsModel> GetDetails(Guid orderId)
    {
        var order = await _ordersRepository.GetSingleOrDefault(
            new OrderDetailsByOrderIdSpecification(orderId));

        return _mapper.Map<OrderDetailsModel>(order);
    }

    private OrdersSearchData GetOrdersSearchData(OrdersSearchModel model)
    {
        var data = _mapper.Map<OrdersSearchData>(model);
        data.FilterPredicate = _entityFilterQueryBuilder.BuildQuery(model);

        return data;
    }
}