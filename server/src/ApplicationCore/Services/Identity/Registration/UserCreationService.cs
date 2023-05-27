using ApplicationCore.Entities;
using ApplicationCore.Entities.InitialData;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Users;
using ApplicationCore.Models.Identity;
using ApplicationCore.Services.Dependencies.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Services.Identity.Registration;

[ScopedDependency]
internal class UserCreationService : IUserCreationService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserPasswordService _userPasswordService;
    private readonly IMapper _mapper;
    private readonly IUserCreationAvailabilityChecker _userCreationAvailabilityChecker;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Assignment> _assignmentRepository;
    private readonly IRepository<PersonalData> _personalDataRepository;

    public UserCreationService(
        IRepository<User> userRepository,
        IUserPasswordService userPasswordService,
        IMapper mapper,
        IUserCreationAvailabilityChecker userCreationAvailabilityChecker,
        UserManager<User> userManager,
        IRepository<Assignment> assignmentRepository,
        IRepository<PersonalData> personalDataRepository)
    {
        _userRepository = userRepository;
        _userPasswordService = userPasswordService;
        _mapper = mapper;
        _userCreationAvailabilityChecker = userCreationAvailabilityChecker;
        _userManager = userManager;
        _assignmentRepository = assignmentRepository;
        _personalDataRepository = personalDataRepository;
    }

    public async Task<Assignment> Create(UserRegistrationModel model)
    {
        if (!(await _userCreationAvailabilityChecker.Check(model.Email)).IsSucceeded)
        {
            throw new InvalidInputException($"User with this {model.Email} already exists");
        }

        var createdUser = _mapper.Map<User>(model);
        var assignment = await CreateAssignment(model, createdUser);

        _userPasswordService.AssignUserPassword(createdUser, model.Password);
        
        await _userRepository.Add(createdUser);

        return assignment;
    }

    private async Task<Assignment> CreateAssignment(UserRegistrationModel model, User user)
    {
        var assignment = new Assignment();
        assignment.UserId = user.Id;
        assignment.User = user;
        assignment.RoleId = RolesInitData.ConsumerRoleId;
        assignment.Phone = model.Phone;
        assignment.CreatedByUserId = UsersInitData.SystemUserId;

        var personalData = new PersonalData
        {
            Id = Guid.NewGuid(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            AddressId = default,
            Address = null,
            Birthday = null,
            Email = model.Email,
            Gender = null,
            Phone = model.Phone,
            Assignment = assignment,
            LegalDetailsId = null,
            LegalDetails = null,
        };

        assignment.PersonalDataId = personalData.Id;
        assignment.PersonalData = personalData;

        return await _assignmentRepository.Add(assignment);
    }
}