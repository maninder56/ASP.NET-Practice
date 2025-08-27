using Microsoft.AspNetCore.Mvc.Filters;

namespace CommunityBoardAPI.Filters;

public class LogResourceFilterAttribute : Attribute, IResourceFilter
{
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        
        Console.WriteLine("Executed!");
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine("Executing!");
    }
}
