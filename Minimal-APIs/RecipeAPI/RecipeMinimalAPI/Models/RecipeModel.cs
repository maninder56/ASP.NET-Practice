
namespace RecipeMinimalAPI.Models; 

public class RecipeModel
{
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public DateOnly? DateCreated { get; set; }

    public RecipeModel() { }
}
