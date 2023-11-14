using FinanceSystem.Abstractions.Models.Users;
using FinanceSystem.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[Tags("User")]
[Route("api/users")]
public sealed class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="userRegisterDto">Registration user info</param>
    /// <returns>Guid - new user Id</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public Task<IActionResult> RegisterUser(UserRegisterDto userRegisterDto) =>
        GetResult(async () => await _userService.RegisterUser(userRegisterDto));

    /// <summary>
    /// Authorize user
    /// </summary>
    /// <param name="userLoginDto">Email and password</param>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public Task<IActionResult> AuthorizeUser(UserLoginDto userLoginDto) =>
        GetResult(async () => await _userService.AuthorizeUser(userLoginDto));
}