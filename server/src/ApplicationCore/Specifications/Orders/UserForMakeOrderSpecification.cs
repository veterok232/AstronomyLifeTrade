using ApplicationCore.Entities;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.Orders;

public class UserForMakeOrderSpecification : Specification<User>
{
    public UserForMakeOrderSpecification(Guid userId)
        : base(u => u.Id == userId)
    {
        AddIncludes(
            u => u.Assignment,
            u => u.Assignment.PersonalData,
            u => u.Assignment.PersonalData.Address);
    }
}