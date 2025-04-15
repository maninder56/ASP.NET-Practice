using RecipeDatabaseContext;

namespace RecipeMinimalAPI.Services;

public class RecipeDataBaseService : IRecipeDataBaseService
{
    private readonly RecipeDataContext database; 

    public RecipeDataBaseService (RecipeDataContext database)
    {
        this.database = database;
    }

    public List<Recipe> GetAllRecipes()
    {
        return database.Recipies.ToList();
    }

    public Recipe? GetRecipeByID(int id)
    {
        return database.Recipies.FirstOrDefault(r => r.RecipeId == id);
    }

}
