using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;

namespace SchoolAPI.Services.Implementations; 

public class BaseDatabaseService
{
    private protected SchoolForApiContext database; 

    public BaseDatabaseService(SchoolForApiContext database)
    {
        this.database = database;
    }

    // Helper Methods

    // Department Helper Methods
    public int MaximumDepartmentID()
    {
        int? id = database.Departments?.AsNoTracking()
            .Max(d => d.DepartmentId);

        return id ?? 0;
    }

    public bool DepartmentExists(int departmentID)
    {
        return database.Departments?.AsNoTracking()
            .Any(d => d.DepartmentId == departmentID)
            ?? false; 
    }


    // Course Helper Methods 
    public bool CourseExists(int courseId)
    {
        return database.Courses?.AsNoTracking()
            .Any(c => c.CourseId == courseId)
            ?? false;
    }

    public int MaximumCourseID()
    {
        return database.Courses?.AsNoTracking()
            .Max(c => c.CourseId) 
            ?? 0; 
    }


    // OnlineCourse Helper Methods 
    public bool OnlineCourseExists(int courseID)
    {
        return database.OnlineCourses?.AsNoTracking()
            .Any(oc => oc.CourseId == courseID)
            ?? false;
    }

    // OnsiteCourse Helper Methods 
    public bool OnsiteCourseExists(int courseID)
    {
        return database.OnsiteCourses?.AsNoTracking()
            .Any(os => os.CourseId == courseID)
            ?? false; 
    }


    // OfficeAssignment Helper Methods 
    public bool OfficeAssignmentExists(int InstructorId)
    {
        return database.OfficeAssignments?.AsNoTracking()
            .Any(o => o.InstructorId == InstructorId)
            ?? false;
    }

    // Person Helper Methods 
    public bool PersonExists(int PersonId)
    {
        return database.Person?.AsNoTracking()
            .Any(p => p.PersonId == PersonId)  
            ?? false;
    }



}
