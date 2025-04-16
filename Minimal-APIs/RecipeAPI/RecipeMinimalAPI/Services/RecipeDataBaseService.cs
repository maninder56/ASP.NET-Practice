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
            .Select(r => new Recipe()
            {
                RecipeId = r.RecipeId,
                RecipeName = r.RecipeName,
                DateCreated = r.DateCreated,
                Ingredients = r.Ingredients
                    .Select(i => new Ingredient()
                    {
                        IngredientId = i.IngredientId,
                        IngredientName = i.IngredientName,
                        Quantity = i.Quantity,
                        Unit = i.Unit,
                    }).ToList()
            })
            .ToList();
    }

    public Recipe? GetRecipeByID(int id)
    {
        return database.Recipes
            .AsNoTracking()
            .Select(r => new Recipe()
            {
                RecipeId = r.RecipeId,
                RecipeName = r.RecipeName,
                DateCreated = r.DateCreated, 
                Ingredients = r.Ingredients
                    .Select(i => new Ingredient()
                    {
                        IngredientId = i.IngredientId,
                        IngredientName = i.IngredientName,
                        Quantity = i.Quantity,
                        Unit = i.Unit,
                    }).ToList()
            })
            .FirstOrDefault(r => r.RecipeId == id);
    }

}
