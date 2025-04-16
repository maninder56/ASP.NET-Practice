
namespace RecipeMinimalAPI.ValidationFilters;

public class IDValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0); 

        if (id < 1)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>()
            {
                { "id", new string[] { "Invalid ID, ID can not be less than 1" } }
            }); 
        }

        return await next(context); 
    }
}
