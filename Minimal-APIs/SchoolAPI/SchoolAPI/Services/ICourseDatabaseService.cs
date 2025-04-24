using DatabaseContext;

namespace SchoolAPI.Services; 

public interface ICourseDatabaseService
{
    // Read Operations
    public List<Course> GetAllCourses(string type);
    public Course? GetCourseByID(int id);

    // Create Operations
    public Course? CreateCourse(Course course);

    // Update Operations
    public Course? UpdateCourseByID(int id, Course course);

    // Delete Operations
    public bool DeleteCourseByID(int id);
}
