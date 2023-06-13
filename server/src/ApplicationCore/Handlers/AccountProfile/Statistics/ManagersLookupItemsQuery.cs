using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.Statistics;

public record ManagersLookupItemsQuery(ManagersLookupItemsQueryModel Model)
    : IRequest<IReadOnlyList<NamedObject>>;