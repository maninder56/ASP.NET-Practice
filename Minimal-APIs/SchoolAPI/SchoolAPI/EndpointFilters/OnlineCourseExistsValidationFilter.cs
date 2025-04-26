
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Services;

namespace SchoolAPI.EndpointFilters;

public class OnlineCourseExistsValidationFilter : IEndpointFilter
{
    IOnlineCoursesDatabaseService dbServie; 

    public OnlineCourseExistsValidationFilter([FromServices] IOnlineCoursesDatabaseService dbServie)
    {
        this.dbServie = dbServie;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0); 

        if (!dbServie.OnlineCourseExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new string[] { $"Course with id {id} does not exists" } }
            });
        }

        return await next(context);
    }
}
