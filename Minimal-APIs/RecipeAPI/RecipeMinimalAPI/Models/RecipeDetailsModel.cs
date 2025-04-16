using RecipeDatabaseContext;

namespace RecipeMinimalAPI.Models; 

public class RecipeDetailsModel : RecipeModel
{
    public RecipeDetailsModel() { }

    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
