using DatabaseContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.EndpointFilters;
using SchoolAPI.Services;

namespace SchoolAPI.Endpoints; 

public static class CourseEndpoints
{
    public static WebApplication MapCourseEndpoints(this WebApplication app)
    {
        RouteGroupBuilder endpoint = app.MapGroup("/course");

        RouteGroupBuilder endpointWithValidation = endpoint.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>()
            .AddEndpointFilter<CourseExistsValidationFilter>(); 

        // GET Endpoints
        endpoint.MapGet("/{type:alpha=all}", GetAllCourses); 
        endpointWithValidation.MapGet("/{id:int}", GetCourseByID)
            .WithName("GetCourseByID");

        // POST Endpoints
        endpoint.MapPost("/", CreateCourse)
            .WithParameterValidation();

        // PUT Endpoints
        endpointWithValidation.MapPut("/{id:int}", UpdateCourseByID)
            .WithParameterValidation();

        // DELETE Endpoints
        endpointWithValidation.MapDelete("/{id:int}", DeleteCourseByID); 

        return app; 
    }

    // Handlers
    
    // GET Handlers
    private static Results<Ok<List<Course>>,ProblemHttpResult> GetAllCourses(
        string type, [FromServices] ICourseDatabaseService dbService)
    {
        List<Course> courses = dbService.GetAllCourses(type);

        return TypedResults.Ok(courses);
    }

    private static Results<Ok<Course>, ProblemHttpResult> GetCourseByID(
        int id, [FromServices] ICourseDatabaseService dbService)
    {
        Course? course = dbService.GetCourseByID(id);

        if (course == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to retrive course");
        }

        return TypedResults.Ok(course);
    }


    // POST Handlers
    private static Results<Created<Course>, ProblemHttpResult> CreateCourse(
        Course course, [FromServices] ICourseDatabaseService dbService, [FromServices] LinkGenerator linkGenerator)
    {
        Course? created = dbService.CreateCourse(course);

        if (created == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to created Course"); 
        }

        string? link = linkGenerator.GetPathByName("GetCourseByID", new { id = created.CourseId }); 

        return TypedResults.Created(link, created);
    }


    // PUT Handlers
    private static Results<NoContent, ProblemHttpResult> UpdateCourseByID(
        int id, Course course, [FromServices] ICourseDatabaseService dbService)
    {
        Course? updatedCourse = dbService.UpdateCourseByID(id, course);

        if (updatedCourse == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to update course"); 
        }

        return TypedResults.NoContent(); 
    }


    // DELETE Handlers
    private static Results<NoContent, ProblemHttpResult> DeleteCourseByID(
        int id, [FromServices] ICourseDatabaseService dbService)
    {
        bool deleted = dbService.DeleteCourseByID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to delete Course"); 
        }

        return TypedResults.NoContent();
    }


}
