using System;
using System.Collections.Generic;

namespace RecipeDatabaseContext;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public decimal? Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public int? RecipeId { get; set; }

    public virtual Recipy? Recipe { get; set; }
}
