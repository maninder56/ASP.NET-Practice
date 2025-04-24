using SchoolAPI.Services.Implementations; 

namespace SchoolAPI.Services; 

public static class DatabaseOperationsServiceCollectionExtension
{
    public static IServiceCollection AddDatabaseOperations(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentDatabaseService, DepartmentDatabaseService>(); 
        services.AddScoped<IOnsiteCoursesDatabaseService, OnsiteCoursesDatabaseService>();
        services.AddScoped<IOnlineCoursesDatabaseService, OnlineCoursesDatabaseService>(); 

        return services; 
    }
}
