using Microsoft.AspNetCore.Http.HttpResults;
using RecipeDatabaseContext; 

namespace RecipeMinimalAPI.Services;

public interface IRecipeEndpointHandlersService
{
    // GET Methods
    public Results<Ok<List<Recipe>>, ProblemHttpResult> GetAllRecipies();
    public Results<Ok<Recipe>, ProblemHttpResult> GetRecipieByID(int id);

}
