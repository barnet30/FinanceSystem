using FinanceSystem.Abstractions.Models.Result;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinanceSystem.Filters;

public class ValidationActionFilter<T> : IActionFilter where T: class
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var item = context.ActionArguments.FirstOrDefault(x => x.Value is T).Value as T;
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        
        if (validator == null)
            return;

        var result = validator.Validate(item);
        if (result.IsValid)
            return;

        var response = Result.NotValid(string.Join(", ", result.Errors));
        context.Result = new JsonResult(response);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // ignore
    }
}