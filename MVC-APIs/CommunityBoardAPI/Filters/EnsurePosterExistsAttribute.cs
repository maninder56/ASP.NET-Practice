using CommunityBoardAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CommunityBoardAPI.Filters; 

public class EnsurePosterExistsAttribute : TypeFilterAttribute
{
    public EnsurePosterExistsAttribute()
        :base(typeof(EnsurePosterExistsFilter)) { }

    public class EnsurePosterExistsFilter : IActionFilter
    {
        private IPosterService service;

        public EnsurePosterExistsFilter(IPosterService service)
        {
            this.service = service;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Ensure id");
            int? posterId = (int?)context.ActionArguments["id"];

            if (posterId is null)
            {
                return;
            }

            if (!service.PosterExists((int)posterId))
            {
                context.Result = new NotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }

}

