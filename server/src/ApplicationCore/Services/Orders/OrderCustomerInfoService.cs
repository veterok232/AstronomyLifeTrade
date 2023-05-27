using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Interfaces.Orders;
using ApplicationCore.Models.Orders;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Specifications.Orders;
using AutoMapper;

namespace ApplicationCore.Services.Orders;

[ScopedDependency]
public class OrderCustomerInfoService : IOrderCustomerInfoService
{
    private readonly IAuthContextAccessor _authContextAccessor;
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public OrderCustomerInfoService(
        IAuthContextAccessor authContextAccessor,
        IRepository<User> userRepository,
        IMapper mapper)
    {
        _authContextAccessor = authContextAccessor;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<OrderCustomerInfo> Get()
    {
        var user = await _userRepository.GetSingleOrDefault(
            new UserForMakeOrderSpecification(_authContextAccessor.UserId.Value));

        return _mapper.Map<OrderCustomerInfo>(user);
    }
}