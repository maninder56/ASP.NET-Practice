
using BasicFruitsAPI.Services;

namespace BasicFruitsAPI.ValidationFilters;

public class IdValidationFilter : IEndpointFilter
{
    private FruitsService _fruitsService; 

    public IdValidationFilter(FruitsService fruitsService)
    {
        _fruitsService = fruitsService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int id = context.GetArgument<int>(0);

        if (id < 1 || !(_fruitsService.DoesFruitIDExists(id)))
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                {"id", new string[] { "Invalid ID", $"Fruit with ID {id} does not exists." } }
            });
        }

        return await next(context); 
    }
}

