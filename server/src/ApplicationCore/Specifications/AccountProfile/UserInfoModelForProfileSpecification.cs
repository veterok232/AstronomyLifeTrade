using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Specifications.Common;

namespace ApplicationCore.Specifications.AccountProfile;

public class UserInfoModelForProfileSpecification : DataTransformSpecification<Assignment, UserInfoModel>
{
    public UserInfoModelForProfileSpecification(Guid assignmentId)
        : base(
            a => new UserInfoModel
            {
                FirstName = a.PersonalData.FirstName,
                LastName = a.PersonalData.LastName,
                Phone = a.PersonalData.Phone,
                Email = a.PersonalData.Email,
                Address = new AddressModel
                {
                    Building = a.PersonalData.Address.Building,
                    City = a.PersonalData.Address.City,
                    Country = a.PersonalData.Address.Country,
                    Flat = a.PersonalData.Address.Flat,
                    PostalCode = a.PersonalData.Address.PostalCode,
                    Street = a.PersonalData.Address.Street,
                    FullAddress = a.PersonalData.Address.FullAddress,
                },
                Birthday = a.PersonalData.Birthday.Value.Date,
                Gender = a.PersonalData.Gender,
            },
            a => a.Id == assignmentId)
    {
        AddIncludes(
            a => a.PersonalData,
            a => a.PersonalData.Address);
    }
}