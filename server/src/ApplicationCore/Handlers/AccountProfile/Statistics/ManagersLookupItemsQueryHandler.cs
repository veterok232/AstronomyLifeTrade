using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Common;
using ApplicationCore.Specifications.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.Statistics;

internal class ManagersLookupItemsQueryHandler : IRequestHandler<ManagersLookupItemsQuery, IReadOnlyList<NamedObject>>
{
    private readonly IRepository<Assignment> _assignmentsRepository;

    public ManagersLookupItemsQueryHandler(IRepository<Assignment> assignmentsRepository)
    {
        _assignmentsRepository = assignmentsRepository;
    }

    public Task<IReadOnlyList<NamedObject>> Handle(
        ManagersLookupItemsQuery query,
        CancellationToken cancellationToken)
    {
        return _assignmentsRepository.List(new ManagersLookupItemSpecification(query.Model));
    }
}