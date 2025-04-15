using Microsoft.EntityFrameworkCore;
using RecipeDatabaseContext;
using Microsoft.Data.SqlClient; 

namespace RecipeMinimalAPI.Services; 

public static class ServiceCollectionExtensions
{
    private static SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".\\LEARNING", 
        InitialCatalog = "RecipeData", 
        IntegratedSecurity = true,
        ConnectTimeout = 5, 
        Encrypt = true,
        TrustServerCertificate = true,
    };

    public static IServiceCollection AddRecipeServices(this IServiceCollection services)
    {
        services.AddDbContext<RecipeDataContext>(options
            => options.UseSqlServer(connectionStringBuilder.ConnectionString)); 

        services.AddScoped<IRecipeDataBaseService, RecipeDataBaseService>(); 
        services.AddScoped<IRecipeEndpointHandlersService, RecipeEndpointHandlersService>();

        return services;
    }
}
