using static System.Console; 
using RecipeDatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
{
    DataSource = ".\\LEARNING",
    InitialCatalog = "RecipeData",
    IntegratedSecurity = true,
    ConnectTimeout = 5,
    Encrypt = true,
    TrustServerCertificate = true,
};



DbContextOptions<RecipeDataContext> options = new DbContextOptionsBuilder<RecipeDataContext>()
    .UseSqlServer(connectionStringBuilder.ConnectionString)
    .Options; 


RecipeDataContext db = new RecipeDataContext(options);


List<Recipe> recipes = db.Recipes
    .AsNoTracking()
    .Select(r => new Recipe()
    {
        RecipeId = r.RecipeId, 
        RecipeName = r.RecipeName,
        DateCreated = r.DateCreated, 
        Ingredients = r.Ingredients.ToList()
    })
    .ToList();




foreach (var recipe in recipes)
{
    WriteLine(recipe.RecipeId);
    WriteLine(recipe.RecipeName); 
    WriteLine(recipe.DateCreated);

    foreach(var i in recipe.Ingredients)
    {
        WriteLine(i.IngredientName.PadLeft(30));
    }
    
    WriteLine();

}
