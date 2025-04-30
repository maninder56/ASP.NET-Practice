using DatabaseContext;
using Microsoft.EntityFrameworkCore; 

namespace SchoolAPI.Services.Implementations;

public class StudentGradeDatabaseService : BaseDatabaseService, IStudentGradeDatabaseService
{
    public StudentGradeDatabaseService(SchoolForApiContext database) 
        : base(database) { }

    // CRUD Operations 

    // Read Operations
    public List<StudentGrade> GetAllStudentGrades()
    {
        return database.StudentGrades?.AsNoTracking()
            .ToList() ?? new List<StudentGrade>();
    }

    public StudentGrade? GetStudentGradeByID(int enrollmentID)
    {
        return database.StudentGrades?.AsNoTracking()
            .Where(sg => sg.EnrollmentId == enrollmentID)
            .FirstOrDefault();
    }


    // Create Operations
    public StudentGrade? CreateStudentGrade(StudentGrade studentGrade)
    {
        database.StudentGrades?.Add(studentGrade);

        return database.SaveChanges() > 0 ? studentGrade : null;
    }


    // Update Operations
    public bool UpdateStudentGradeByID(int enrollmentID, StudentGrade studentGrade)
    {
        studentGrade.EnrollmentId = enrollmentID;   

        database.StudentGrades?.Update(studentGrade);

        return database.SaveChanges() > 0; 
    }


    // Delete Operations
    public bool DeleteStudentGradeByID(int enrollmentID)
    {
        StudentGrade? studentGrade = GetStudentGradeByID(enrollmentID);

        if (studentGrade == null)
        {
            return false;
        }

        database.StudentGrades?.Remove(studentGrade);

        return database.SaveChanges() > 0;
    }
}
