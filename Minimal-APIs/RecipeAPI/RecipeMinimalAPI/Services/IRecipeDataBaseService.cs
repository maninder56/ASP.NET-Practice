using RecipeDatabaseContext;
using RecipeMinimalAPI.Models;

namespace RecipeMinimalAPI.Services; 

public interface IRecipeDataBaseService
{
    public List<RecipeModel> GetAllRecipes();
    public RecipeModel? GetRecipeByID(int id);
}
