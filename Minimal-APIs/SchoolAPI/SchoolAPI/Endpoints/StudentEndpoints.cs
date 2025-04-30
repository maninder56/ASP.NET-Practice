using DatabaseContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.EndpointFilters;
using SchoolAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Endpoints; 

public static class StudentEndpoints
{
    private record StudentRecord
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        public DateTime? EnrollmentDate { get; set; }

        public StudentRecord() { }

        public StudentRecord(Person person)
        {
            StudentId = person.PersonId;
            LastName = person.LastName;
            FirstName = person.FirstName;   
            EnrollmentDate = person.EnrollmentDate;
        }
    }

    public static WebApplication MapStudentEndpoints(this WebApplication app)
    {
        RouteGroupBuilder endpoint = app.MapGroup("/student");

        RouteGroupBuilder endpointWithValidation = endpoint.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>()
            .AddEndpointFilter<StudentExistsValidationFilter>();

        // GET Endpoints 
        endpoint.MapGet("/", GetAllStudents); 
        endpointWithValidation.MapGet("/{id:int}", GetStudentByID)
            .WithName("GetStudentByID");

        // POST Endpoints 
        endpoint.MapPost("/", CreateStudent)
            .WithParameterValidation(); 

        // PUT Endpoints 
        endpointWithValidation.MapPut("/{id:int}", UpdateStudentByID)
            .WithParameterValidation();

        // DELETE Endpoints 
        endpointWithValidation.MapDelete("/{id:int}", DeleteStudentByID); 

        return app; 
    }


    // Handlers

    // GET Handlers
    private static Results<Ok<List<StudentRecord>>, ProblemHttpResult> GetAllStudents(
        [FromServices] IStudentDatabaseService dbService)
    {
        List<StudentRecord> instructorRecords = dbService.GetAllStudents()
            .Select(p => new StudentRecord(p))
            .ToList();

        return TypedResults.Ok(instructorRecords);
    }

    private static Results<Ok<StudentRecord>, ProblemHttpResult> GetStudentByID(
        int id, [FromServices] IStudentDatabaseService dbService)
    {
        Person? student = dbService.GetStudentbyID(id);

        if (student == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to retrive Student details");
        }

        return TypedResults.Ok(new StudentRecord(student));
    }


    //// POST Handlers 
    private static Results<Created<StudentRecord>, ProblemHttpResult> CreateStudent(
        StudentRecord studentRecord, [FromServices] IStudentDatabaseService dbService,
        [FromServices] LinkGenerator linkGenerator)
    {
        Person? created = dbService.CreateSudent(new Person()
        {
            FirstName = studentRecord.FirstName,
            LastName = studentRecord.LastName,
        }); 

        if (created == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to created Student");
        }

        string? link = linkGenerator.GetPathByName("GetStudentByID", new { id = created.PersonId });

        return TypedResults.Created(link, new StudentRecord(created));
    }


    // PUT Handlers
    private static Results<NoContent, ProblemHttpResult> UpdateStudentByID(
        int id, StudentRecord studentRecord, [FromServices] IStudentDatabaseService dbService)
    {
        Person personDetails = dbService.GetStudentbyID(id)!;

        personDetails.FirstName = studentRecord.FirstName;
        personDetails.LastName = studentRecord.LastName;

        if (studentRecord.EnrollmentDate != null)
        {
            personDetails.EnrollmentDate = studentRecord.EnrollmentDate;
        }

        bool updated = dbService.UpdateStudentByID(id, personDetails);

        if (!updated)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to update Student Details");
        }

        return TypedResults.NoContent();
    }


    // DELETE Handlers 
    private static Results<NoContent, ProblemHttpResult, ValidationProblem> DeleteStudentByID(
        int id, [FromServices] IStudentDatabaseService dbService)
    {
        bool studentInStudentGrade = dbService.StudentInStudentGradeExists(id);

        if (studentInStudentGrade)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                { "StudentID", new string[] { $"Student with ID {id} can not be deleted because it is being referenced by StudentGrade" } }
            });
        }

        bool deleted = dbService.DeleteStudentByID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to delete Student");
        }

        return TypedResults.NoContent();
    }

}
