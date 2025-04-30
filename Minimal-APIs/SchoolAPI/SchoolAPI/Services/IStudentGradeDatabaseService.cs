using DatabaseContext;

namespace SchoolAPI.Services; 

public interface IStudentGradeDatabaseService
{
    // Helper Methods 
    public bool StudentGradeExists(int enrollmentID);
    public bool CourseExists(int courseId);
    public bool StudentExists(int studentId); 

    // Read Operations
    public List<StudentGrade> GetAllStudentGrades(); 
    public StudentGrade? GetStudentGradeByID(int enrollmentID);

    // Create Operations
    public StudentGrade? CreateStudentGrade(StudentGrade studentGrade);

    // Update Operations
    public bool UpdateStudentGradeByID(int enrollmentID, StudentGrade studentGrade);

    // Delete Operations
    public bool DeleteStudentGradeByID(int enrollmentID);
}
