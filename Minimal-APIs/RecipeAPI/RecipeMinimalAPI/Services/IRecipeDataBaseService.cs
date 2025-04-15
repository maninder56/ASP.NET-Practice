using RecipeDatabaseContext;

namespace RecipeMinimalAPI.Services; 

public interface IRecipeDataBaseService
{
    public List<Recipe> GetAllRecipes();
    public Recipe? GetRecipeByID(int id);
}
