using RecipeDatabaseContext;

namespace RecipeMinimalAPI.Services;

public class RecipeDataBaseService : IRecipeDataBaseService
{
    private RecipeDataContext database; 

    public RecipeDataBaseService (RecipeDataContext database)
    {
        this.database = database;
    }

    public List<Recipe> GetAllRecipes()
    {
        return database.Recipes.ToList();
    }

    public Recipe? GetRecipeByID(int id)
    {
        return database.Recipes.FirstOrDefault(r => r.RecipeId == id);
    }

}
