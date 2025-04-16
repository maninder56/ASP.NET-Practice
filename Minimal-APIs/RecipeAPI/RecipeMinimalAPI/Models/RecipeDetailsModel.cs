
namespace RecipeMinimalAPI.Models; 

public class RecipeDetailsModel
{
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public DateOnly? DateCreated { get; set; }

    public RecipeDetailsModel() { }

    public List<IngredientModel> Ingredients { get; set; } = new List<IngredientModel>();
}
