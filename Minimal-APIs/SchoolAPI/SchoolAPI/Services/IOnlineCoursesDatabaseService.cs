using DatabaseContext;

namespace SchoolAPI.Services; 

public interface IOnlineCoursesDatabaseService
{
    // Helper Methods 
    public bool CourseExists(int courseId); 

    // Read Operations
    public List<OnlineCourse> GetAllOnlineCourses();
    public OnlineCourse? GetOnlineCourseByID(int courseId);

    // Create Operations
    public OnlineCourse? CreateOnlineCourse(OnlineCourse onlineCourse);

    // Update Operations
    public OnlineCourse? UpdateOnlineCourseByID(int id, OnlineCourse onlineCourse);

    // Delete Operations
    public bool DeleteOnlineCourseByID(int courseId);   
}
