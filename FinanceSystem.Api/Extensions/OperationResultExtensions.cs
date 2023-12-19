using System.Net;
using FinanceSystem.Abstractions.Models.Result;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Extensions;

public static class OperationResultExtensions
{
    public static ProblemDetails ToProblemDetails<T>(this Result<T> result, string instance = null)
    {
        if (result.IsSuccess)
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.OK
            };

        var status = result.Errors.Type switch
        {
            OperationErrorType.Internal => HttpStatusCode.InternalServerError,
            OperationErrorType.Validation => HttpStatusCode.BadRequest,
            OperationErrorType.NotFound => HttpStatusCode.NotFound,
            OperationErrorType.Forbidden => HttpStatusCode.Forbidden,
            OperationErrorType.Unauthorized => HttpStatusCode.Unauthorized,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var problemDetails = new ProblemDetails
        {
            Status = (int)status,
            Title = result.Errors.Type.ToDescription(),
            Instance = instance
        };
        
        foreach (var (key, value) in result.Errors.Values)
            problemDetails.Extensions.TryAdd(key, value);

        return problemDetails;
    }

    public static ProblemDetails ToProblemDetails(this Result result, string instance = null)
    {
        if (result.IsSuccess)
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.OK
            };

        var status = result.Errors.Type switch
        {
            OperationErrorType.Internal => HttpStatusCode.InternalServerError,
            OperationErrorType.Validation => HttpStatusCode.BadRequest,
            OperationErrorType.NotFound => HttpStatusCode.NotFound,
            OperationErrorType.Forbidden => HttpStatusCode.Forbidden,
            OperationErrorType.Unauthorized => HttpStatusCode.Unauthorized,
            _ => throw new ArgumentOutOfRangeException()
        };

        var problemDetails = new ProblemDetails
        {
            Type = result.Errors.Type.ToType(),
            Status = (int)status,
            Title = result.Errors.Type.ToDescription(),
            Instance = instance,
            Extensions =
            {
                ["errors"] = result.Errors.Values
            }
        };
        
        return problemDetails;
    }
}