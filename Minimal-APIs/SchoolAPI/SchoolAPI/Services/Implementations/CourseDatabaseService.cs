using DatabaseContext;
using Microsoft.EntityFrameworkCore; 

namespace SchoolAPI.Services.Implementations;

public class CourseDatabaseService : BaseDatabaseService, ICourseDatabaseService
{
    public CourseDatabaseService(SchoolForApiContext database) 
        : base(database) { }

    // CRUD Operations on Course Table

    // Read Operations 
    public List<Course> GetAllCourses(string type)
    {
        return type switch
        {
            "online" => database.Courses?.AsNoTracking()
                .Where(c => c.OnlineCourse != null)
                .ToList() ?? new List<Course>(), 

            "onsite" => database.Courses?.AsNoTracking()
                .Where(c => c.OnsiteCourse != null)
                .ToList() ?? new List<Course>(),

            _ => database.Courses?.AsNoTracking()
                .ToList() ?? new List<Course>()
        }; 
    }

    public Course? GetCourseByID(int id)
    {
        return database.Courses?.AsNoTracking()
            .Where(c => c.CourseId == id)
            .FirstOrDefault();
    }


    // Create Operations 
    public Course? CreateCourse(Course course)
    {
        course.CourseId = MaximumCourseID() + 1; 

        database.Courses?.Add(course);

        return database.SaveChanges() > 0 ? course : null;
    }


    // Update Operations 
    public Course? UpdateCourseByID(int id, Course course)
    {
        course.CourseId = id; 

        database.Courses?.Update(course);

        return database.SaveChanges() > 0 ? course : null; 
    }

    // Delete Operations 
    public bool DeleteCourseByID(int id)
    {
        Course? course = GetCourseByID(id);

        if (course is null)
        {
            return false;
        }

        database.Courses?.Remove(course);

        return database.SaveChanges() > 0; 
    }

    
}
