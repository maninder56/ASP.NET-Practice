using SchoolAPI.Data;
using SchoolAPI.Services.Implementations; 

namespace SchoolAPI.Services; 

public static class DatabaseOperationsServiceCollectionExtension
{
    public static IServiceCollection AddDatabaseOperationsServices(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentDatabaseService, DepartmentDatabaseService>(); 
        services.AddScoped<IOnsiteCoursesDatabaseService, OnsiteCoursesDatabaseService>();
        services.AddScoped<IOnlineCoursesDatabaseService, OnlineCoursesDatabaseService>(); 
        services.AddScoped<ICourseDatabaseService, CourseDatabaseService>();
        services.AddScoped<IOfficeAssignmentDatabaseService, OfficeAssignmentDatabaseService>();
        services.AddScoped<IInstructorDatabaseService, InstructorDatabaseService>();

        return services; 
    }
}
