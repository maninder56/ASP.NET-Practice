using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeDatabaseContext;
using RecipeMinimalAPI.Services;
using RecipeMinimalAPI.ValidationFilters;

namespace RecipeMinimalAPI;

public static class EndpointExtensionMethods
{
    public static WebApplication AddRecipeEndpoints(this WebApplication app)
    {
        RouteGroupBuilder recipeApi = app.MapGroup("/recipe");

        RouteGroupBuilder recipeApiWithIDValidation = recipeApi.MapGroup("/")
            .AddEndpointFilter<IDValidationFilter>();

        recipeApi.MapGet("/all", ([FromServices] IRecipeEndpointHandlersService handler)
            => handler.GetAllRecipies());

        recipeApiWithIDValidation.MapGet("/{id:int}", (int id, [FromServices] IRecipeEndpointHandlersService handler)
            => handler.GetRecipieByID(id));

        return app;
    }
}
