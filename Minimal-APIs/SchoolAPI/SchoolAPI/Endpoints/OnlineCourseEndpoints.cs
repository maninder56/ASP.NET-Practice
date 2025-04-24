using DatabaseContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.EndpointFilters;
using SchoolAPI.Services;
using System.Data;
using System.Runtime.CompilerServices;

namespace SchoolAPI.Endpoints; 

public static class OnlineCourseEndpoints
{
    public static WebApplication MapOnlineCourseEndpoints(this WebApplication app)
    {
        RouteGroupBuilder endpoint = app.MapGroup("/onlinecourse");

        RouteGroupBuilder endpointWithValidation = endpoint.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>();

        // GET Endpoints 
        endpoint.MapGet("/", GetAllOnlineCourses); 
        endpointWithValidation.MapGet("/{id:int}", GetOnlineCourseByID)
            .WithName("OnlineCourseByID");

        // POST Endpoints 
        endpoint.MapPost("/", CreateOnlineCourses)
            .WithParameterValidation(); 

        // PUT Endpoints 
        endpointWithValidation.MapPut("/{id:int}", UpdateOnlineCoursebyID)
            .WithParameterValidation();

        // DELETE Endpoints 
        endpointWithValidation.MapDelete("/{id:int}", DeleteOnlineCourseByID); 

        return app; 
    }

    // Handlers 

    // GET Handlers 
    private static Results<Ok<List<OnlineCourse>>, ProblemHttpResult> GetAllOnlineCourses(
        [FromServices] IOnlineCoursesDatabaseService dbService)
    {
        List<OnlineCourse> onlineCourses = dbService.GetAllOnlineCourses();

        return TypedResults.Ok(onlineCourses);
    }

    private static Results<Ok<OnlineCourse>, ProblemHttpResult> GetOnlineCourseByID(
        int id, [FromServices] IOnlineCoursesDatabaseService dbService)
    {
        OnlineCourse? course = dbService.GetOnlineCourseByID(id);

        if (course == null)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Course with id {id} does not exist");
        }

        return TypedResults.Ok(course);

    }

    // POST Handlers 
    private static Results<Created<OnlineCourse>, ProblemHttpResult> CreateOnlineCourses(
        OnlineCourse onlineCourse, [FromServices] IOnlineCoursesDatabaseService dbService,
        [FromServices] LinkGenerator linkGenerator)
    {
        if (!dbService.CourseExists(onlineCourse.CourseId))
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Can not create Online details for course that does not exists");
        }

        OnlineCourse? created = dbService.CreateOnlineCourse(onlineCourse); 

        if (created is null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to create online course");
        }

        string? link = linkGenerator.GetPathByName("OnlineCourseByID", new { id = created.CourseId }); 

        return TypedResults.Created(link, created);
    }


    // PUT Handlers 
    private static Results<NoContent, ProblemHttpResult> UpdateOnlineCoursebyID(
        int id, OnlineCourse onlineCourse, [FromServices] IOnlineCoursesDatabaseService dbService)
    {
        bool entityExists = dbService.GetOnlineCourseByID(id) != null;

        if (!entityExists)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Online course with id {id} does not exist");
        }

        OnlineCourse? updatedCourse = dbService.UpdateOnlineCourseByID(id, onlineCourse);

        if (updatedCourse is null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to update online course");
        }

        return TypedResults.NoContent();
    }

    // DELETE Handlers 
    private static Results<NoContent, ProblemHttpResult> DeleteOnlineCourseByID(
        int id, [FromServices] IOnlineCoursesDatabaseService dbService)
    {
        bool entityExists = dbService.GetOnlineCourseByID(id) != null;

        if (!entityExists)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Online course with id {id} does not exist");
        }

        bool deleted = dbService.DeleteOnlineCourseByID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: $"Failed to process given request");
        }

        return TypedResults.NoContent();
    }

}
