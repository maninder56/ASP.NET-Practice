using DatabaseContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Data; 

public static class DatabaseServiceCollectionExtension
{
    public static IServiceCollection AddSchoolDatabaseService(this IServiceCollection services)
    {
        services.AddDbContext<SchoolForApiContext>(options
            => options.UseSqlServer(new SqlConnectionStringBuilder()
            {
                DataSource = ".\\LEARNING",
                InitialCatalog = "SchoolForAPI",
                IntegratedSecurity = true,
                ConnectTimeout = 5,
                Encrypt = true,
                TrustServerCertificate = true
            }.ConnectionString));

        return services; 
    }
}
