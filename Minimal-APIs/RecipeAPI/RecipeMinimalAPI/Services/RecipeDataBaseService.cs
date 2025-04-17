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
        return database.Recipes.AsNoTracking()
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
        return database.Recipes.AsNoTracking()
            .Select(r => new RecipeModel()
            {
                RecipeId = r.RecipeId,
                RecipeName = r.RecipeName,
                DateCreated = r.DateCreated
            })
            .FirstOrDefault(r => r.RecipeId == id);
    }

    public RecipeDetailsModel? GetRecipeDetailsByID(int id)
    {
        return database.Recipes.AsNoTracking()
            .Select(r => new RecipeDetailsModel()
            {
                RecipeId = r.RecipeId,
                RecipeName = r.RecipeName,
                DateCreated = r.DateCreated, 
                Ingredients = r.Ingredients
                    .Select(i => new IngredientModel()
                    {
                        IngredientId = i.IngredientId,
                        IngredientName = i.IngredientName,
                        Quantity = i.Quantity,
                        Unit = i.Unit,
                    }).ToList()
            })
            .FirstOrDefault(r => r.RecipeId == id);
    }



    public RecipeModel? CreateOnlyRecipe(string recipeName)
    {
        int recipeID = database.Recipes.Max(r => r.RecipeId) + 1; 
        DateOnly recipeDate = DateOnly.FromDateTime(DateTime.Now);

        Recipe recipeEntity = new Recipe()
        {
            RecipeId = recipeID,
            RecipeName = recipeName,
            DateCreated = recipeDate
        }; 

        database.Add(recipeEntity);

        int entityChanges = database.SaveChanges();

        if (entityChanges == 0)
        {
            return null; 
        }

        RecipeModel createdRecipe = new RecipeModel()
        {
            RecipeId = recipeEntity.RecipeId,
            RecipeName = recipeEntity.RecipeName,
            DateCreated = recipeEntity.DateCreated
        }; 

        return createdRecipe; 
    }
}
