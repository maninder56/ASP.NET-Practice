
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Services;

namespace SchoolAPI.EndpointFilters;

public class InstructorExistsValidationFilter : IEndpointFilter
{
    private IInstructorDatabaseService dbService;

    public InstructorExistsValidationFilter([FromServices] IInstructorDatabaseService dbService)
    {
        this.dbService = dbService;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0);

        if (!dbService.InstructorExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new string[] { $"Instructor with id {id} does not exists" } }
            });
        }

        return await next(context);
    }
}
