using DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Services.Implementations; 

public class BaseDatabaseService
{
    private protected SchoolForApiContext database; 

    public BaseDatabaseService(SchoolForApiContext database)
    {
        this.database = database;
    }

    // Helper Methods

    public int MaximumDepartmentID()
    {
        int? id = database.Departments?.AsNoTracking()
            .Max(d => d.DepartmentId);

        return id ?? 0;
    }


    public bool CourseExists(int courseId)
    {
        return database.Courses?.AsNoTracking()
            .Any(c => c.CourseId == courseId)
            ?? false;
    }
}
