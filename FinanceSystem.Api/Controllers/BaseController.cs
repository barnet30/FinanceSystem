using System.Net;
using FinanceSystem.Abstractions.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    public async Task<IActionResult> GetResult(Func<Task<Result>> action,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        var result = await action();

        return new ObjectResult(result.ToProblemDetails())
        {
            StatusCode = result.IsSuccess ? (int)successStatusCode : (int)result.Errors.Type
        };
    }

    public async Task<IActionResult> GetResult<T>(Func<Task<Result<T>>> action, HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        var result = await action();

        return new ObjectResult(result.IsSuccess ? result.Data : result.ToProblemDetails())
        {
            StatusCode = result.IsSuccess ? (int)successStatusCode : (int)result.Errors.Type
        };
    }
}