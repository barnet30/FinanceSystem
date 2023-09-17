using FinanceSystem.Abstractions.Models.Users;
using FinanceSystem.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[Tags("User")]
public sealed class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    //public Task<IActionResult> RegisterUser(UserRegisterDto userRegisterDto) => GetResult(()=>
}