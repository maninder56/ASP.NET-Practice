using DatabaseContext;
using Microsoft.EntityFrameworkCore; 

namespace SchoolAPI.Services.Implementations; 

public class StudentDatabaseService : BaseDatabaseService, IStudentDatabaseService
{
    public StudentDatabaseService(SchoolForApiContext database)
        : base(database) { }

    // CRUD Operations 

    // Read Operations
    public List<Person> GetAllStudents()
    {
        return database.Person?.AsNoTracking()
            .Where(p => p.Discriminator == "Student")
            .ToList() ?? new List<Person>();
    }

    public Person? GetStudentbyID(int studentId)
    {
        return database.Person?.AsNoTracking()
            .Where(p => p.PersonId == studentId && p.Discriminator == "Student")
            .FirstOrDefault();
    }


    // Create Operations 
    public Person? CreateSudent(Person student)
    {
        student.EnrollmentDate = DateTime.Now;
        student.Discriminator = "Student"; 

        database.Person?.Add(student);

        return database.SaveChanges() > 0 ? student : null;
    }


    // Update Operations 
    public bool UpdateStudentByID(int studentId, Person student)
    {
        student.PersonId = studentId;
        student.Discriminator = "Student"; 

        database.Person?.Update(student);

        return database.SaveChanges() > 0; 
    }


    // Delete Operations 
    public bool DeleteStudentByID(int studentId)
    {
        Person? student = GetStudentbyID(studentId);

        if (student == null)
        {
            return false;
        }

        database.Person?.Remove(student);

        return database.SaveChanges() > 0;
    }

    

    

    
}
