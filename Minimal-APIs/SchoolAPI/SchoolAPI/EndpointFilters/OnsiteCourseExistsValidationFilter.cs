
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Services;

namespace SchoolAPI.EndpointFilters;

public class OnsiteCourseExistsValidationFilter : IEndpointFilter
{
    IOnsiteCoursesDatabaseService dbServie;

    public OnsiteCourseExistsValidationFilter([FromServices] IOnsiteCoursesDatabaseService dbServie)
    {
        this.dbServie = dbServie;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0);

        if (!dbServie.OnsiteCourseExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new string[] { $"Online Course with id {id} does not exists" } }
            });
        }

        return await next(context);
    }
}
