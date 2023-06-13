using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Models.Common;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.Statistics;

public record UsersLookupItemsQuery(UsersLookupItemsQueryModel Model)
    : IRequest<IReadOnlyList<NamedObject>>;