using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Comments;

public class CommentByProductIdSpecification : Specification<Comment>
{
    public CommentByProductIdSpecification(Guid productId)
        : base(c => c.ProductId == productId)
    {
        AddIncludes(
            c => c.Assignment,
            c => c.Assignment.PersonalData);
    }
}