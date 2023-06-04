using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AccountProfile;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Models;
using ApplicationCore.Models.AccountProfile;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.AccountProfile;
using AutoMapper;

namespace ApplicationCore.Services.AccountProfile;

[ScopedDependency]
public class AccountProfileService : IAccountProfileService
{
    private readonly IRepository<Assignment> _assignmentRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IAuthContextAccessor _authContextAccessor;
    private readonly IRepository<PersonalData> _personalDataRepository;
    private readonly IMapper _mapper;

    public AccountProfileService(
        IRepository<Assignment> assignmentRepository,
        IAuthContextAccessor authContextAccessor,
        IRepository<PersonalData> personalDataRepository,
        IMapper mapper,
        IRepository<User> userRepository)
    {
        _assignmentRepository = assignmentRepository;
        _authContextAccessor = authContextAccessor;
        _personalDataRepository = personalDataRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserInfoModel> GetUserInfo()
    {
        var userInfo = await _assignmentRepository.GetSingleOrDefault(
            new UserInfoModelForProfileSpecification(_authContextAccessor.AssignmentId.Value));

        return userInfo;
    }

    public async Task SaveUserInfo(SaveUserInfoModel model)
    {
        var user = await _userRepository.GetById(_authContextAccessor.UserId.Value);
        
        var personalData = await _personalDataRepository.GetSingleOrDefault(
            new PersonalDataByAssignmentIdSpecification(_authContextAccessor.AssignmentId.Value));

        if (user.Email != model.Email)
        {
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _userRepository.Update(user);
        }

        _mapper.Map(model, personalData);

        await _personalDataRepository.Update(personalData);
    }

    public async Task SaveUserAddress(AddressModel model)
    {
        var personalData = await _personalDataRepository.GetSingleOrDefault(
            new PersonalDataByAssignmentIdSpecification(_authContextAccessor.AssignmentId.Value));

        if (!personalData.AddressId.HasValue)
        {
            personalData.Address = _mapper.Map<Address>(model);
        }
        else
        {
            _mapper.Map(model, personalData.Address);
        }
        
        await _personalDataRepository.Update(personalData);
    }
}