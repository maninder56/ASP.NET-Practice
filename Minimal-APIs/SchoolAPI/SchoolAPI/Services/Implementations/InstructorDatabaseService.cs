using DatabaseContext;
using Microsoft.EntityFrameworkCore; 

namespace SchoolAPI.Services.Implementations; 

public class InstructorDatabaseService : BaseDatabaseService, IInstructorDatabaseService
{
    public InstructorDatabaseService(SchoolForApiContext database)
        : base(database) { }

    // CRUD Operations

    // Read Operations 
    public List<Person> GetAllInstructors()
    {
        return database.Person?.AsNoTracking()
            .Where(p => p.Discriminator == "Instructor")
            .ToList() ?? new List<Person>();
    }

    public Person? GetInstructorByID(int instructorID)
    {
        return database.Person?.AsNoTracking()
            .Where(p => p.Discriminator == "Instructor" && p.PersonId == instructorID)
            .FirstOrDefault();
    }


    // Create Operations 
    public Person? CreateInstructor(Person instructor)
    {
        //instructor.PersonId = MaximumPersonID() + 1;
        instructor.HireDate = DateTime.Now;
        instructor.Discriminator = "Instructor"; 

        database.Person?.Add(instructor);

        return database.SaveChanges() > 0 ? instructor : null; 
    }


    // Update Operations 
    public bool UpdateInstructorByID(int instructorID, Person instructor)
    {
        instructor.PersonId = instructorID;
        instructor.Discriminator = "Instructor"; 

        database.Person?.Update(instructor);

        return database.SaveChanges() > 0; 
    }


    // Delete Operations 
    public bool DeleteInstructorByID(int instructorID)
    {
        Person? instructor = GetInstructorByID(instructorID);

        if (instructor == null)
        {
            return false;
        }

        database.Person?.Remove(instructor);

        return database.SaveChanges() > 0;
    }
}
