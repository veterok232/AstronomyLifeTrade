using ApplicationCore.Models.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.Statistics;

public record GetOrdersStatisticsQuery(StatisticsQuery query) : IRequest<List<OrdersAggregatedDataItem>>;