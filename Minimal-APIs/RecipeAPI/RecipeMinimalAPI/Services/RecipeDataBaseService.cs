using Microsoft.EntityFrameworkCore;
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
        return database.Recipes
            .AsNoTracking()
            .ToList();
    }

    public Recipe? GetRecipeByID(int id)
    {
        return database.Recipes
            .AsNoTracking ()
            .FirstOrDefault(r => r.RecipeId == id);
    }

}
