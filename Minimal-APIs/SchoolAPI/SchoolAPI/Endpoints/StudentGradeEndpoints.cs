using DatabaseContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.EndpointFilters;
using SchoolAPI.Services;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

using static SchoolAPI.Data.SchoolRecords.StudentGradeRecords; 

namespace SchoolAPI.Endpoints; 

public static class StudentGradeEndpoints
{
    public static WebApplication MapStudentGradeEndpoints(this WebApplication app)
    {
        RouteGroupBuilder endpoint = app.MapGroup("/student-grade");

        RouteGroupBuilder endpointWithValidation = endpoint.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>()
            .AddEndpointFilter<StudentGradeValidationFilters.StudentGradeExistsValidationFilter>();

        // GET Endpoints 
        endpoint.MapGet("/", GetAllStudentGrades);
        endpointWithValidation.MapGet("/{id:int}", GetStudentGradeByID)
            .WithName("GetStudentGradeByID");

        // POST Endpoints 
        endpoint.MapPost("/", CreateStudentGrade)
            .WithParameterValidation()
            .AddEndpointFilter<StudentGradeValidationFilters.StudentGradeModelValidaitonFilter>();

        // PUT Endpoints 
        endpointWithValidation.MapPut("/{id:int}", UpdateStudentGradeByID)
            .WithParameterValidation(); 

        // DELETE Endpoints
        endpointWithValidation.MapDelete("{id:int}", DeleteStudentGradeByID);   

        return app; 
    }

    // Handlers 

    // GET Handlers 
    private static Results<Ok<List<StudentGradeRecord>>, ProblemHttpResult> GetAllStudentGrades(
        [FromServices] IStudentGradeDatabaseService dbService)
    {
        List<StudentGradeRecord> studentGradeRecords = dbService.GetAllStudentGrades()
            .Select(s =>  new StudentGradeRecord(s))
            .ToList();

        return TypedResults.Ok(studentGradeRecords);
    }

    private static Results<Ok<StudentGradeRecord>, ProblemHttpResult> GetStudentGradeByID(
        int id, [FromServices] IStudentGradeDatabaseService dbService)
    {
        StudentGrade? studentGrade = dbService.GetStudentGradeByID(id); 

        if (studentGrade == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to retrive Student Grade details");
        }

        return TypedResults.Ok(new  StudentGradeRecord(studentGrade));  
    }


    // POST Handlers 
    private static Results<Created<StudentGradeRecord>, ProblemHttpResult> CreateStudentGrade(
        StudentGradeRecord studentGradeRecord, [FromServices] IStudentGradeDatabaseService dbService,
        [FromServices] LinkGenerator linkGenerator)
    {
        StudentGrade? created = dbService.CreateStudentGrade(new StudentGrade()
        {
            CourseId = studentGradeRecord.CourseId,
            StudentId = studentGradeRecord.StudentId
        });

        if (created == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to created Student Grade");
        }

        string? link = linkGenerator.GetPathByName("GetStudentGradeByID", new { id = created.EnrollmentId });

        return TypedResults.Created(link, new StudentGradeRecord(created));
    }


    // PUT Handlers 
    private static Results<NoContent, ProblemHttpResult> UpdateStudentGradeByID(
        int id, StudentGradeRecord studentGradeRecord, [FromServices] IStudentGradeDatabaseService dbService)
    {
        StudentGrade studentGradeDetails = dbService.GetStudentGradeByID(id)!;

        studentGradeDetails.StudentId = studentGradeRecord.StudentId;
        studentGradeDetails.CourseId = studentGradeRecord.CourseId;

        if (studentGradeRecord.Grade != null)
        {
            studentGradeDetails.Grade = studentGradeRecord.Grade;
        }

        bool updated = dbService.UpdateStudentGradeByID(id, studentGradeDetails);

        if (!updated)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to update Student Grade Details");
        }

        return TypedResults.NoContent(); 
    }


    // DELETE Handlers 
    private static Results<NoContent, ProblemHttpResult> DeleteStudentGradeByID(
        int id, [FromServices] IStudentGradeDatabaseService dbService)
    {
        bool deleted = dbService.DeleteStudentGradeByID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to delete Student Grade");
        }

        return TypedResults.NoContent();
    }
}
