
using SchoolAPI.Services;

namespace SchoolAPI.EndpointFilters;

public class StudentExistsValidationFilter : IEndpointFilter
{
    private IStudentDatabaseService dbService; 

    public StudentExistsValidationFilter(IStudentDatabaseService dbService)
    {
        this.dbService = dbService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0);

        if (!dbService.StudentExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new string[] { $"Student with id {id} does not exists" } }
            });
        }

        return await next(context);
    }
}
