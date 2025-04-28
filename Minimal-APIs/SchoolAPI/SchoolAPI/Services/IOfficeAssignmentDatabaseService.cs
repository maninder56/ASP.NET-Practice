using DatabaseContext;

namespace SchoolAPI.Services; 

public interface IOfficeAssignmentDatabaseService
{
    // Helper Methods 
    public bool OfficeAssignmentExists(int instructorID);
    public bool PersonExists(int instructorID); 

    // Read Operations 
    public List<OfficeAssignment> GetAllOfficeAssignments();
    public OfficeAssignment? GetOfficeAssignmentById(int instructorID);

    // Create Operations 
    public OfficeAssignment? CreateOffiAssignment(OfficeAssignment newOfficeAssignment);

    // Update Operations 
    public bool UpdateOfficeAssignmentByID(int instructorID, OfficeAssignment officeAssignment);

    // Delete Operations 
    public bool DeleteOfficeAssignmentByID(int instructorID);
}
