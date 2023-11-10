using Authorization.Interfaces;
using AutoMapper;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Abstractions.Models.Users;
using FinanceSystem.Common.Constants;
using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Users;
using FinanceSystem.Services.Interfaces.Users;

namespace FinanceSystem.Services.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthManager _authManager;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IAuthManager authManager
    )
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _authManager = authManager;
    }

    public async Task<Result<Guid>> RegisterUser(UserRegisterDto userRegisterDto)
    {
        var user = _mapper.Map<User>(userRegisterDto);
        var result = await _userRepository.InsertAsync(user);
        return Result<Guid>.FromValue(result);
    }

    public async Task<Result<string>> AuthorizeUser(UserLoginDto userLoginDto)
    {
        var existUser = await _userRepository.GetByEmailAndPassword(userLoginDto.Email, userLoginDto.Password);
        if (existUser == null)
            return Result<string>.NotFound(UserConstants.WrongEmailOrPass);

        var token = _authManager.GenerateJwt(existUser);
        return token.IsSuccess ? Result<string>.FromValue(token.Data) : token;
    }
}