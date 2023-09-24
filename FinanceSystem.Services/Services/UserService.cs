using AutoMapper;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Abstractions.Models.Users;
using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Users;
using FinanceSystem.Services.Interfaces.Users;

namespace FinanceSystem.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> RegisterUser(UserRegisterDto userRegisterDto)
    {
        var user = _mapper.Map<User>(userRegisterDto);
        var result = await _userRepository.InsertAsync(user);
        return Result<Guid>.FromValue(result);
    }
}