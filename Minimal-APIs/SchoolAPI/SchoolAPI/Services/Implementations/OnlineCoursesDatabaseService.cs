using DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Services.Implementations;

public class OnlineCoursesDatabaseService : BaseDatabaseService, IOnlineCoursesDatabaseService
{
    public OnlineCoursesDatabaseService(SchoolForApiContext database)
        :base(database) { }

    // CRUD Operations on OnlineCourse Table

    // Read Operations 
    public List<OnlineCourse> GetAllOnlineCourses()
    {
        return database.OnlineCourses?.AsNoTracking()
            .ToList() ?? new List<OnlineCourse>();
    }

    public OnlineCourse? GetOnlineCourseByID(int courseId)
    {
        return database.OnlineCourses?.AsNoTracking()
            .Where(c => c.CourseId == courseId)
            .FirstOrDefault();  
    }


    // Create Operations 
    public OnlineCourse? CreateOnlineCourse(OnlineCourse newOnlineCourse)
    {
        database.OnlineCourses?.Add(newOnlineCourse);

        int entityAdded = database.SaveChanges();

        if (entityAdded > 0)
        {
            return newOnlineCourse; 
        }
        else
        {
            return null;
        }
    }

    // Update Operations 
    public OnlineCourse? UpdateOnlineCourseByID(int id, OnlineCourse onlineCourse)
    {
        onlineCourse.CourseId = id;

        database.OnlineCourses?.Update(onlineCourse);

        int entityAdded = database.SaveChanges();

        if (entityAdded > 0)
        {
            return onlineCourse;
        }
        else
        {
            return null;
        }
    }

    // Delete Operations 

    public bool DeleteOnlineCourseByID(int courseId)
    {
        OnlineCourse? courseToDelete = GetOnlineCourseByID(courseId); 

        if (courseToDelete is null)
        {
            return false; 
        }

        database.OnlineCourses?.Remove(courseToDelete);

        int entityAdded = database.SaveChanges();

        return entityAdded > 0;
    }

    

    
}
