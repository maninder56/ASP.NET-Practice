
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Services;

namespace SchoolAPI.EndpointFilters;

public class CourseExistsValidationFilter : IEndpointFilter
{
    private ICourseDatabaseService dbService; 

    public CourseExistsValidationFilter([FromServices] ICourseDatabaseService dbService)
    {
        this.dbService = dbService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0);

        if (!dbService.CourseExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new string[] { $"Course with id {id} does not exists" } }
            });
        }

        return await next(context);
    }
}
