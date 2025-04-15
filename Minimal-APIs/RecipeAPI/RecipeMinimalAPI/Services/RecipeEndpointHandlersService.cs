using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeDatabaseContext;

namespace RecipeMinimalAPI.Services;

public class RecipeEndpointHandlersService : IRecipeEndpointHandlersService
{
    IRecipeDataBaseService dataBaseService; 

    public RecipeEndpointHandlersService(IRecipeDataBaseService recipeDataBaseService)
    {
        dataBaseService = recipeDataBaseService;
    }

    public Results<Ok<List<Recipe>>, ProblemHttpResult> GetAllRecipies() 
    {
        List<Recipe> recipieList = dataBaseService.GetAllRecipes();
        
        if (recipieList.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404, detail: "No Recipe found"); 
        }

        return TypedResults.Ok(recipieList);
    }

    public Results<Ok<Recipe>, ProblemHttpResult> GetRecipieByID(int id)
    {
        Recipe? recipe = dataBaseService.GetRecipeByID(id); 

        if (recipe == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: $"Recipe with ID {id} does not exists"); 
        }

        return TypedResults.Ok(recipe);
    }
}
