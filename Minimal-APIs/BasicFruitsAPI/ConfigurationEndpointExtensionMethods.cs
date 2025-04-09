using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BasicFruitsAPI; 

public static class ConfigurationEndpointExtensionMethods
{
    public static WebApplication AddConfigurationEndpoints(this WebApplication app)
    {
        RouteGroupBuilder configurationApi = app.MapGroup("/configurations");

        configurationApi.MapGet("/all", GetAllConfigurations); 

        return app; 
    }

    
    // Endpoint Handlers 

    private static Results<Ok<IEnumerable<KeyValuePair<string,string?>>>, ProblemHttpResult> GetAllConfigurations(
        [FromServices] IConfiguration configurationService)
    {
        IEnumerable<KeyValuePair<string,string?>>? configurationList = configurationService.AsEnumerable();

        if (!configurationList.Any())
        {
            return TypedResults.Problem(statusCode: 404, detail: "No Configuration Available"); 
        }

        return TypedResults.Ok(configurationList);
    }
}
