using ApplicationCore.Models.AccountProfile;

namespace ApplicationCore.Interfaces.AccountProfile;

public interface IStatisticsService
{
    Task<List<OrdersAggregatedDataItem>> GetOrdersStatistics(StatisticsQuery query);
}