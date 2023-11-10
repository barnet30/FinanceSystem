using FinanceSystem.Abstractions.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[Route("api/test")]
[Authorize]
public class TestController : BaseController
{
    [HttpGet("isAuthorized")]
    [AllowAnonymous]
    public Task<IActionResult> ShowIfAuthorized()
    {
        var isAuthorized = AuthorizedUserId != default;

        return GetResult(() =>
            Task.FromResult(Result<string>.FromValue(isAuthorized
                ? $"I am authorized with id {AuthorizedUserId}"
                : "I am not authorized")));
    }
}