using System.Linq.Expressions;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Models.Common;
using ApplicationCore.Specifications.Common;
using ApplicationCore.Utils;

namespace ApplicationCore.Specifications.AccountProfile;

public class ManagersLookupItemSpecification : DataTransformSpecification<Assignment, NamedObject>
{
    private static readonly Expression<Func<Assignment, NamedObject>> TransformExpression = a =>
        new NamedObject()
        {
            Id = a.Id,
            Name = $"{a.PersonalData.LastName} {a.PersonalData.FirstName} - {a.PersonalData.Email}",
        };

    public ManagersLookupItemSpecification(ManagersLookupItemsQueryModel model)
        : base(TransformExpression, GetSelectExpression(model))
    {
        ApplyOrderBy(a => a.PersonalData.LastName);
        ApplyPaging(1, 20);
        if (model.SelectedId.HasValue)
        {
            AddOutOfPagingCriteria(p => model.SelectedId == p.Id);
        }
    }

    private static Expression<Func<Assignment, bool>> GetSelectExpression(ManagersLookupItemsQueryModel model)
    {
        var builder = new PredicateBuilder<Assignment>();

        if (!string.IsNullOrEmpty(model.SearchValue))
        {
            builder.And(p => p.PersonalData.LastName.ToLower().StartsWith(model.SearchValue.ToLowerInvariant()));
        }

        if (model.SelectedId.HasValue)
        {
            builder.And(p => model.SelectedId != p.Id);
        }
        
        builder.And(a => a.Role.Name == Roles.Manager);

        return builder.Build();
    }
}