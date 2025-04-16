using Microsoft.EntityFrameworkCore;
using RecipeDatabaseContext;
using RecipeMinimalAPI.Models; 

namespace RecipeMinimalAPI.Services;

public class RecipeDataBaseService : IRecipeDataBaseService
{
    private RecipeDataContext database; 

    public RecipeDataBaseService (RecipeDataContext database)
    {
        this.database = database;
    }

    public List<RecipeModel> GetAllRecipes()
    {
        return database.Recipes
            .AsNoTracking()
            .Select(r => new RecipeModel()
            {
                RecipeId = r.RecipeId,
                RecipeName = r.RecipeName,
                DateCreated = r.DateCreated,
            })
            .ToList();
    }

    public RecipeModel? GetRecipeByID(int id)
    {
        return database.Recipes
            .AsNoTracking()
            .Select(r => new RecipeModel()
            {
                RecipeId = r.RecipeId,
                RecipeName = r.RecipeName,
                DateCreated = r.DateCreated
            })
            .FirstOrDefault(r => r.RecipeId == id);
    }

}
