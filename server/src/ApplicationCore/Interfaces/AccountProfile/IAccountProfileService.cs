using ApplicationCore.Models;
using ApplicationCore.Models.AccountProfile;

namespace ApplicationCore.Interfaces.AccountProfile;

public interface IAccountProfileService
{
    Task<UserInfoModel> GetUserInfo();
    
    Task SaveUserInfo(SaveUserInfoModel model);
    
    Task SaveUserAddress(AddressModel model);
}