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
            .AddEndpointFilter<IdValidationFilter>();

        // GET Endpoints
        endpoint.MapGet("/{type:alpha=all}", GetAllCourses); 
        endpointWithValidation.MapGet("/{id:int}", GetCourseByID);

        // Endpoints
        // Endpoints
        // Endpoints

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
            return TypedResults.Problem(statusCode: 400, detail: $"Course with id {id} does not exist");
        }

        return TypedResults.Ok(course);
    }


    // Handlers
    // Handlers
    // Handlers



}
