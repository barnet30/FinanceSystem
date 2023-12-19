using AutoMapper;
using FinanceSystem.Abstractions.Models.Result;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Extensions;

public static class ExceptionExtensions
{
    public static ProblemDetails ToProblemDetail(this Exception exception, IHostEnvironment env)
    {
        var responseMessage = OperationErrorType.Internal.ToDescription();

        if (!env.IsEnvironment("Production"))
            responseMessage = exception?.ToString();

        return exception.ToResult(responseMessage).ToProblemDetails();
    }
    
    public static Result ToResult(this Exception exception, string responseMessage)
    {
        return exception switch
        {
            _ => Result.Internal(responseMessage)
        };
    }
}