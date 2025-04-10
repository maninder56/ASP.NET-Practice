using BasicFruitsAPI.ConfigurationClasses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BasicFruitsAPI; 

public static class ConfigurationEndpointExtensionMethods
{
    public static WebApplication AddConfigurationEndpoints(this WebApplication app)
    {
        RouteGroupBuilder configurationApi = app.MapGroup("/configurations");

        configurationApi.MapGet("/all", GetAllConfigurations); 

        configurationApi.MapGet("/userOne", GetUserOneUserSecret);

        configurationApi.MapGet("/apikey", GetApiKey); 

        configurationApi.MapGet("/mockconfiguration", GetMockConfiguration);

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


    private static Results<Ok<UserOne>, ProblemHttpResult> GetUserOneUserSecret(
        [FromServices] IOptions<UserOne> options)
    {
        UserOne? userOne = options.Value;

        if (userOne == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: "No User secrets available"); 
        }

        return TypedResults.Ok(userOne);
    }

    private static Results<Ok<ExternalAPI>, ProblemHttpResult> GetApiKey(
        [FromServices] IOptions<ExternalAPI> options)
    {
        ExternalAPI api = options.Value;    

        if (api.ApiKey == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: "No Api Key available"); 
        }

        return TypedResults.Ok(api);
    }

    private static Results<Ok<MockConfigurations>, ProblemHttpResult> GetMockConfiguration(
        [FromServices] IOptions<MockConfigurations> options)
    {
        MockConfigurations? configuration = options.Value;

        if (configuration == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: "No Mock Information Available");
        }

        return TypedResults.Ok(configuration);
    }
}
