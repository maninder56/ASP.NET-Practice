using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeDatabaseContext;
using RecipeMinimalAPI.Models; 

namespace RecipeMinimalAPI.Services;

public class RecipeEndpointHandlersService : IRecipeEndpointHandlersService
{
    IRecipeDataBaseService dataBaseService; 

    public RecipeEndpointHandlersService(IRecipeDataBaseService recipeDataBaseService)
    {
        dataBaseService = recipeDataBaseService;
    }

    public Results<Ok<List<RecipeModel>>, ProblemHttpResult> GetAllRecipies() 
    {
        List<RecipeModel> recipieList = dataBaseService.GetAllRecipes();
        
        if (recipieList.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404, detail: "No Recipe found"); 
        }

        return TypedResults.Ok(recipieList);
    }

    public Results<Ok<RecipeModel>, ProblemHttpResult> GetRecipieByID(int id)
    {
        RecipeModel? recipe = dataBaseService.GetRecipeByID(id); 

        if (recipe == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: $"Recipe with ID {id} does not exists"); 
        }

        return TypedResults.Ok(recipe);
    }
}
