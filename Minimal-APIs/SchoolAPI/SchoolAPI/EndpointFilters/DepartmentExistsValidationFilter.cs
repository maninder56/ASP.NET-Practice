
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Services;

namespace SchoolAPI.EndpointFilters;

public class DepartmentExistsValidationFilter : IEndpointFilter
{
    private IDepartmentDatabaseService dbService; 

    public DepartmentExistsValidationFilter([FromServices] IDepartmentDatabaseService dbService)
    {
        this.dbService = dbService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0); 

        if (!dbService.DepartmentExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new string[] { $"Department with id {id} does not exists" } }
            });
        }

        return await next(context);
    }
}
