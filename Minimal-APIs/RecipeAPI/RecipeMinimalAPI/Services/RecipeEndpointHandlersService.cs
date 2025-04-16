using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeDatabaseContext;
using RecipeMinimalAPI.Models; 

namespace RecipeMinimalAPI.Services;

public class RecipeEndpointHandlersService : IRecipeEndpointHandlersService
{
    IRecipeDataBaseService dataBaseService; 
    LinkGenerator linkGenerator;

    public RecipeEndpointHandlersService(
        IRecipeDataBaseService recipeDataBaseService, LinkGenerator linkGenerator)
    {
        dataBaseService = recipeDataBaseService;
        this.linkGenerator = linkGenerator;
    }

    // GET Handlers

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

    public Results<Ok<RecipeDetailsModel>, ProblemHttpResult> GetRecipeDetailsByID(int id)
    {
        RecipeDetailsModel? recipeDetails = dataBaseService.GetRecipeDetailsByID(id);

        if (recipeDetails == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: $"Recipe with ID {id} does not exists");
        }

        return TypedResults.Ok(recipeDetails);
    }



    // POST Handlers 

    public Results<Created<RecipeModel>, ProblemHttpResult> CreateOnlyRecipe(RecipeModel recipe)
    {
        RecipeModel? createdRecipe = dataBaseService.CreateOnlyRecipe(recipe);

        if (createdRecipe == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: $"Failed to create Recipe"); 
        }

        string? link = linkGenerator.GetPathByName("recipeOnly", new { id = createdRecipe.RecipeId });

        return TypedResults.Created(link, createdRecipe); 
    }
}
