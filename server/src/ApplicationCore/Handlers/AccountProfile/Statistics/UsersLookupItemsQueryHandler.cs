using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Models.Common;
using ApplicationCore.Specifications.AccountProfile;
using MediatR;

namespace ApplicationCore.Handlers.AccountProfile.Statistics;

internal class UsersLookupItemsQueryHandler : IRequestHandler<UsersLookupItemsQuery, IReadOnlyList<NamedObject>>
{
    private readonly IRepository<Assignment> _assignmentsRepository;
    private readonly IAuthContextAccessor _authContextAccessor;

    public UsersLookupItemsQueryHandler(
        IRepository<Assignment> assignmentsRepository,
        IAuthContextAccessor authContextAccessor)
    {
        _assignmentsRepository = assignmentsRepository;
        _authContextAccessor = authContextAccessor;
    }

    public Task<IReadOnlyList<NamedObject>> Handle(
        UsersLookupItemsQuery query,
        CancellationToken cancellationToken)
    {
        return _assignmentsRepository.List(
            new UsersLookupItemSpecification(query.Model, _authContextAccessor.AssignmentId.Value));
    }
}