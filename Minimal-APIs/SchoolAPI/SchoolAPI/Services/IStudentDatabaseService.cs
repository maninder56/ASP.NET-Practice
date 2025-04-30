using DatabaseContext;

namespace SchoolAPI.Services; 

public interface IStudentDatabaseService
{
    // Helper Methods 
    public bool StudentExists(int studentId);
    public bool StudentInStudentGradeExists(int studentId);

    // Read Operations 
    public List<Person> GetAllStudents(); 
    public Person? GetStudentbyID(int studentId);

    // Create Operations 
    public Person? CreateSudent(Person student); 

    // Update Operations 
    public bool UpdateStudentByID(int studentId, Person student);

    // Delete Operations 
    public bool DeleteStudentByID(int studentId);
}
