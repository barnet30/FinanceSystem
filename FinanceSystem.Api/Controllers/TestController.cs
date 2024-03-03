using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[Route("api/test")]
[Authorize]
public class TestController : BaseController
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

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

    [HttpGet("testGrpc")]
    [AllowAnonymous]
    public Task<IActionResult> TestGrpc(string query) =>
        GetResult(async () => await _testService.SendGrpcRequest(query));

    [HttpGet("testIndex")]
    [AllowAnonymous]
    public Task<IActionResult> IndexPayment(Guid id) => GetResult(async () => await _testService.SendIndexRequest(id));
}