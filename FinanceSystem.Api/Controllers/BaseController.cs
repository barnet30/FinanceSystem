using System.Net;
using System.Security.Claims;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Common.Constants;
using FinanceSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{
    protected Guid? AuthorizedUserId
    {
        get
        {
            var userIdClaim = User.FindFirstValue(AuthorizationConstants.UserIdClaimName);
            if (!string.IsNullOrWhiteSpace(userIdClaim) && Guid.TryParse(userIdClaim, out var userId))
                return userId;

            return null;
        }
    }

    protected async Task<IActionResult> GetResult(Func<Task<Result>> action,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        var result = await action();

        return new ObjectResult(result.ToProblemDetails())
        {
            StatusCode = result.IsSuccess ? (int)successStatusCode : (int)result.Errors.Type
        };
    }

    protected static async Task<IActionResult> GetResult<T>(Func<Task<Result<T>>> action, HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        var result = await action();

        return new ObjectResult(result.IsSuccess ? result.Data : result.ToProblemDetails())
        {
            StatusCode = result.IsSuccess ? (int)successStatusCode : (int)result.Errors.Type
        };
    }
}