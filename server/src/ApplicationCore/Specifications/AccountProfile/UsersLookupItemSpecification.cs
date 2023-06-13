using System.Linq.Expressions;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Models.Common;
using ApplicationCore.Specifications.Common;
using ApplicationCore.Utils;

namespace ApplicationCore.Specifications.AccountProfile;

public class UsersLookupItemSpecification : DataTransformSpecification<Assignment, NamedObject>
{
    private static readonly Expression<Func<Assignment, NamedObject>> TransformExpression = a =>
        new NamedObject()
        {
            Id = a.Id,
            Name = $"{a.PersonalData.LastName} {a.PersonalData.FirstName} - {a.PersonalData.Email}",
        };

    public UsersLookupItemSpecification(UsersLookupItemsQueryModel model, Guid personalAssignmentId)
        : base(TransformExpression, GetSelectExpression(model, personalAssignmentId))
    {
        ApplyOrderBy(a => a.PersonalData.LastName);
        ApplyPaging(1, 20);
        if (model.SelectedId.HasValue)
        {
            AddOutOfPagingCriteria(p => model.SelectedId == p.Id);
        }
    }

    private static Expression<Func<Assignment, bool>> GetSelectExpression(
        UsersLookupItemsQueryModel model,
        Guid personalAssignmentId)
    {
        var builder = new PredicateBuilder<Assignment>();

        if (!string.IsNullOrEmpty(model.SearchValue))
        {
            builder.And(a => a.PersonalData.LastName.ToLower().StartsWith(model.SearchValue.ToLowerInvariant()));
        }

        if (model.SelectedId.HasValue)
        {
            builder.And(a => model.SelectedId != a.Id);
        }
        
        builder.And(a => a.Role.Name != Roles.System);
        builder.And(a => a.Id != personalAssignmentId);

        return builder.Build();
    }
}