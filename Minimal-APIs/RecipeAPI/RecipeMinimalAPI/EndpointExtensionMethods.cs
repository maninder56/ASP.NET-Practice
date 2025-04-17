using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeDatabaseContext;
using RecipeMinimalAPI.Models;
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

        recipeApi.MapGet("/all", 
            ([FromServices] IRecipeEndpointHandlersService handler)
                => handler.GetAllRecipies());

        recipeApiWithIDValidation.MapGet("/{id:int}",
            (int id, [FromServices] IRecipeEndpointHandlersService handler)
                => handler.GetRecipieByID(id))
                .WithName("recipeOnly"); 

        recipeApiWithIDValidation.MapGet("/recipeDetail/{id:int}", 
            (int id, [FromServices] IRecipeEndpointHandlersService handler) 
                => handler.GetRecipeDetailsByID(id));

        recipeApi.MapPost("/",
            ([FromBody] RecipeName recipeName, [FromServices] IRecipeEndpointHandlersService handler)
                => handler.CreateOnlyRecipe(recipeName.Name)); 

        return app;
    }

    private record RecipeName(string Name);
}


