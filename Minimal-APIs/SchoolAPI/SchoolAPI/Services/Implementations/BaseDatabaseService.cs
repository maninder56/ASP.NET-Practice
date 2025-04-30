using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;

namespace SchoolAPI.Services.Implementations; 

public class BaseDatabaseService
{
    private protected SchoolForApiContext database; 

    public BaseDatabaseService(SchoolForApiContext database)
    {
        this.database = database;
    }

    // Helper Methods

    // Department Helper Methods
    public int MaximumDepartmentID()
    {
        int? id = database.Departments?.AsNoTracking()
            .Max(d => d.DepartmentId);

        return id ?? 0;
    }

    public bool DepartmentExists(int departmentID)
    {
        return database.Departments?.AsNoTracking()
            .Any(d => d.DepartmentId == departmentID)
            ?? false; 
    }


    // Course Helper Methods 
    public bool CourseExists(int courseId)
    {
        return database.Courses?.AsNoTracking()
            .Any(c => c.CourseId == courseId)
            ?? false;
    }

    public int MaximumCourseID()
    {
        return database.Courses?.AsNoTracking()
            .Max(c => c.CourseId) 
            ?? 0; 
    }


    // OnlineCourse Helper Methods 
    public bool OnlineCourseExists(int courseID)
    {
        return database.OnlineCourses?.AsNoTracking()
            .Any(oc => oc.CourseId == courseID)
            ?? false;
    }


    // OnsiteCourse Helper Methods 
    public bool OnsiteCourseExists(int courseID)
    {
        return database.OnsiteCourses?.AsNoTracking()
            .Any(os => os.CourseId == courseID)
            ?? false; 
    }


    // OfficeAssignment Helper Methods 
    public bool OfficeAssignmentExists(int instructorId)
    {
        return database.OfficeAssignments?.AsNoTracking()
            .Any(o => o.InstructorId == instructorId)
            ?? false;
    }


    // Person Helper Methods 
    public bool PersonExists(int personId)
    {
        return database.Person?.AsNoTracking()
            .Any(p => p.PersonId == personId)  
            ?? false;
    }

    public int MaximumPersonID()
    {
        return database.Person?.AsNoTracking()
            .Max(c => c.PersonId)
            ?? 0; 
    }


    // Instructor Helper Methods 
    public bool InstructorExists(int instructorId)
    {
        return database.Person?.AsNoTracking()
            .Any(i => i.PersonId == instructorId && i.Discriminator == "Instructor")
            ?? false;
    }

    // Student Helper Methods 
    public bool StudentExists(int studentId)
    {
        return database.Person?.AsNoTracking()
            .Any(s => s.PersonId == studentId && s.Discriminator == "Student")
            ?? false; 
    }


    // Student Grade Helper Methods 
    public bool StudentGradeExists(int enrollmentID)
    {
        return database.StudentGrades?.AsNoTracking()
            .Any(sg =>  sg.EnrollmentId == enrollmentID)
            ?? false;
    }

    public bool StudentInStudentGradeExists(int studentId)
    {
        return database.StudentGrades?.AsNoTracking()
            .Any(sg => sg.StudentId == studentId)
            ?? false;
    }

    public bool CourseInStudentGradeExists(int courseId)
    {
        return database.StudentGrades?.AsNoTracking()
            .Any(sg => sg.CourseId == courseId)
            ?? false;
    }



}
