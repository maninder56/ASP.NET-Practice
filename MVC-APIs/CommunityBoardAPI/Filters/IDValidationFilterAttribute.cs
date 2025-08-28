using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CommunityBoardAPI.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class IDValidationFilterAttribute : Attribute, IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Id_Validation");
        object? argumentValue = null; 

        if (!context.ActionArguments.TryGetValue("id", out argumentValue))
        {
            return; 
        }

        if (argumentValue is not null && argumentValue is int idValue)
        {
            if (idValue < 1)
            {
                context.Result = new BadRequestObjectResult(new ProblemDetails()
                {
                    Title = "id is invalid", 
                    Detail = "id can not be less than 0"
                });
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}
