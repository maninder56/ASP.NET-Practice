using DatabaseContext;

namespace SchoolAPI.Services; 

public interface IOnsiteCoursesDatabaseService
{
    // Helper methods 
    public bool CourseExists(int courseId); 

    // Read Opeartions 
    public List<OnsiteCourse> GetAllOnsiteCourse();
    public OnsiteCourse? GetOnsiteCourseByCourseId(int id);

    // Create Operattions 
    public OnsiteCourse? CreateOnsiteCourse(OnsiteCourse newOnsiteCourse);

    // Update Operations 
    public OnsiteCourse? UpdateOnsiteCourseByCourseID(int id, OnsiteCourse onsiteCourse);

    // Delete Operations 
    public bool DeleteOnsiteCourseByCourseID(int id);
}
