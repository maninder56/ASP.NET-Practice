using RecipeDatabaseContext;



RecipeDataContext db = new RecipeDataContext();


Console.WriteLine(db.Recipes.FirstOrDefault()?.RecipeName); 
