using Microsoft.AspNetCore.Mvc;
using RecipeMinimalAPI.Services;

namespace RecipeMinimalAPI;

public static class EndpointExtensionMethods
{
    public static WebApplication AddRecipeEndpoints(this WebApplication app)
    {
        RouteGroupBuilder recipeApi = app.MapGroup("/recipe");

        recipeApi.MapGet("/all", ([FromServices] IRecipeEndpointHandlersService handler)
            => handler.GetAllRecipies);

        recipeApi.MapGet("/{id:int}", (int id, [FromServices] IRecipeEndpointHandlersService handler)
            => handler.GetRecipieByID(id));

        return app;
    }
}
