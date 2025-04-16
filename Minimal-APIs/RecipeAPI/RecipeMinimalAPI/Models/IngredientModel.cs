namespace RecipeMinimalAPI.Models; 

public class IngredientModel
{
    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public decimal? Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public IngredientModel() { }
}