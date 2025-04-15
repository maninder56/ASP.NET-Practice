using System;
using System.Collections.Generic;

namespace RecipeDatabaseContext;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public DateOnly? DateCreated { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
