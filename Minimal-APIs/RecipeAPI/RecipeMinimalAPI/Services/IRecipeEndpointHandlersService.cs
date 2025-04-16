using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeMinimalAPI.Models; 

namespace RecipeMinimalAPI.Services;

public interface IRecipeEndpointHandlersService
{
    // GET Methods
    public Results<Ok<List<RecipeModel>>, ProblemHttpResult> GetAllRecipies();
    public Results<Ok<RecipeModel>, ProblemHttpResult> GetRecipieByID(int id);
    public Results<Ok<RecipeDetailsModel>, ProblemHttpResult> GetRecipeDetailsByID(int id);

    // POST Methods 
    public Results<Created<RecipeModel>, ProblemHttpResult> CreateOnlyRecipe(RecipeModel recipe);    
}
