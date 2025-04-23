using DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Services.Implementations;

public class OnsiteCoursesDatabaseService : IOnsiteCoursesDatabaseService
{
    private SchoolForApiContext database;

    public OnsiteCoursesDatabaseService(SchoolForApiContext database)
    {
        this.database = database;
    }

    // Helper Methdos 
    public bool CourseExists(int courseId)
    {
        return database.Courses?.AsNoTracking()
            .Any(c => c.CourseId == courseId)
            ?? false;
    }


    // CRUD Operations on OnsiteCourse Table

    // Read Operations 

    public List<OnsiteCourse> GetAllOnsiteCourse()
    {
        return database.OnsiteCourses?.AsNoTracking()
            .ToList() ?? new List<OnsiteCourse>();
    }

    public OnsiteCourse? GetOnsiteCourseByCourseId(int id)
    {
        return database.OnsiteCourses?.AsNoTracking()
            .Where(c  => c.CourseId == id)
            .FirstOrDefault();
    }


    // Create Operations 

    public OnsiteCourse? CreateOnsiteCourse(OnsiteCourse newOnsiteCourse)
    {
        database.OnsiteCourses?.Add(newOnsiteCourse);  

        int entityAdded = database.SaveChanges();

        if (entityAdded > 0)
        {
            return newOnsiteCourse; 
        }
        else
        {
            return null;
        }
    }


    // Update Operations 

    public OnsiteCourse? UpdateOnsiteCourseByCourseID(int id, OnsiteCourse onsiteCourse)
    {
        onsiteCourse.CourseId = id; 

        database.OnsiteCourses?.Update(onsiteCourse);

        int entityAdded = database.SaveChanges();

        if (entityAdded > 0)
        {
            return onsiteCourse;
        }
        else
        {
            return null;
        }
    }


    // Delete Operations 

    public bool DeleteOnsiteCourseByCourseID(int id)
    {
        OnsiteCourse? courseToDelete = GetOnsiteCourseByCourseId(id);

        if (courseToDelete is null)
        {
            return false;
        }

        database.OnsiteCourses?.Remove(courseToDelete);

        int entityAdded = database.SaveChanges();

        return entityAdded > 0;
    }
}
