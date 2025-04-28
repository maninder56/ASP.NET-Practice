using DatabaseContext;
using Microsoft.EntityFrameworkCore; 

namespace SchoolAPI.Services.Implementations;

public class OfficeAssignmentDatabaseService : BaseDatabaseService, IOfficeAssignmentDatabaseService
{
    public OfficeAssignmentDatabaseService(SchoolForApiContext database) 
        : base(database) { }

    // CRUD Operations on OfficeAssignment Table

    // Read Operations 
    public List<OfficeAssignment> GetAllOfficeAssignments()
    {
        return database.OfficeAssignments?.AsNoTracking()
            .ToList() ?? new List<OfficeAssignment>();
    }

    public OfficeAssignment? GetOfficeAssignmentById(int InstructorId)
    {
        return database.OfficeAssignments?.AsNoTracking()
            .Where(o => o.InstructorId == InstructorId)
            .FirstOrDefault();
    }


    // Create  Operations 
    public OfficeAssignment? CreateOffiAssignment(OfficeAssignment newOfficeAssignment)
    {
        newOfficeAssignment.Timestamp = DateTime.Now;

        database.OfficeAssignments?.Add(newOfficeAssignment);

        return database.SaveChanges() > 0 ? newOfficeAssignment : null;
    }


    // Update Operations 
    public bool UpdateOfficeAssignmentByID(int InstructorId, OfficeAssignment officeAssignment)
    {
        officeAssignment.InstructorId = InstructorId;
        officeAssignment.Timestamp = GetOfficeAssignmentById(InstructorId)?.Timestamp ?? DateTime.Now;

        database.OfficeAssignments?.Update(officeAssignment);

        return database.SaveChanges() > 0; 
    }


    // Delete Operations 
    public bool DeleteOfficeAssignmentByID(int InstructorId)
    {
        OfficeAssignment? officeAssignment = GetOfficeAssignmentById(InstructorId);

        if (officeAssignment is null)
        {
            return false;
        }

        database.OfficeAssignments?.Remove(officeAssignment);

        return database.SaveChanges() > 0; 
    }
}
