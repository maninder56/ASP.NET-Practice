using SchoolAPI.Services;

namespace SchoolAPI.EndpointFilters; 

public class OfficeAssignmentExistsValidationFilter : IEndpointFilter
{
    private IOfficeAssignmentDatabaseService dbService; 

    public OfficeAssignmentExistsValidationFilter(IOfficeAssignmentDatabaseService dbService)
    {
        this.dbService = dbService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0);

        if (!dbService.OfficeAssignmentExists(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "InstructorId", new string[] { $"OfficeAssignment with InstructorId {id} does not exists" } }
            });
        }

        return await next(context);
    }
}
