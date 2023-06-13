using ApplicationCore.Handlers.AccountProfile.GetUserInfo;
using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Models.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.Statistics;

internal class GetOrdersStatisticsQueryHandler
    : IRequestHandler<GetOrdersStatisticsQuery, List<OrdersAggregatedDataItem>>
{
    private readonly IStatisticsService _statisticsService;

    public GetOrdersStatisticsQueryHandler(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<List<OrdersAggregatedDataItem>> Handle(
        GetOrdersStatisticsQuery query,
        CancellationToken cancellationToken)
    {
        return await _statisticsService.GetOrdersStatistics(query.query);
    }
}