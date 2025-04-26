using DatabaseContext;

namespace SchoolAPI.Services; 

public interface IOfficeAssignmentDatabaseService
{
    // Helper Methods 
    public bool OfficeAssignmentExists(int InstructorId); 

    // Read Operations 
    public List<OfficeAssignment> GetAllOfficeAssignments();
    public OfficeAssignment? GetOfficeAssignmentById(int InstructorId);

    // Create Operations 
    public OfficeAssignment? CreateOffiAssignment(OfficeAssignment newOfficeAssignment);

    // Update Operations 
    public bool UpdateOfficeAssignmentByID(int InstructorId, OfficeAssignment officeAssignment);

    // Delete Operations 
    public bool DeleteOfficeAssignmentByID(int InstructorId);
}
