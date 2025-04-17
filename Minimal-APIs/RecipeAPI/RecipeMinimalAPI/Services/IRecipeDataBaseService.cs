using RecipeDatabaseContext;
using RecipeMinimalAPI.Models;

namespace RecipeMinimalAPI.Services; 

public interface IRecipeDataBaseService
{
    // Read Operations 
    public List<RecipeModel> GetAllRecipes();
    public RecipeModel? GetRecipeByID(int id);
    public RecipeDetailsModel? GetRecipeDetailsByID(int id);

    // Create Operations 
    public RecipeModel? CreateOnlyRecipe(string recipeName); 
}
