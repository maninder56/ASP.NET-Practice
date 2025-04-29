using DatabaseContext;

namespace SchoolAPI.Services;

public interface IInstructorDatabaseService
{
    // Helper Methods 
    public bool InstructorExists(int instructorId);
    public bool OfficeAssignmentExists(int instructorId); 

    // Read Operations 
    public List<Person> GetAllInstructors();
    public Person? GetInstructorByID(int instructorID); 

    // Create Operations 
    public Person? CreateInstructor(Person instructor);

    // Update Operations 
    public bool UpdateInstructorByID(int instructorID, Person instructor);

    // Delete Operations 
    public bool DeleteInstructorByID(int instructorID);
}
