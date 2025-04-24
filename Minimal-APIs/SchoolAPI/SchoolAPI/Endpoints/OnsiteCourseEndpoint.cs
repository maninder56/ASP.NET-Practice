using DatabaseContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.EndpointFilters;
using SchoolAPI.Services;

namespace SchoolAPI.Endpoints; 

public static class OnsiteCourseEndpoint
{
    public static WebApplication MapOnsiteCourseEndpoints(this WebApplication app)
    {
        RouteGroupBuilder endpoint = app.MapGroup("/onsitecourse"); 

        RouteGroupBuilder endpointWithValidation = endpoint.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>();

        // GET Endpoints
        endpoint.MapGet("/", GetAllOnsiteCourses); 
        endpointWithValidation.MapGet("/{id:int}", GetOnsiteCourseById);

        // POST Endpoints 
        endpoint.MapPost("/", CreateOnsiteCourse)
            .WithParameterValidation(); 

        // PUT Endpoints
        endpoint.MapPut("/{id:int}", UpdateOnsiteCourseByID)
            .WithParameterValidation();

        // DELETE Endpoints
        endpoint.MapDelete("/{id:int}", DeleteOnsiteCourseById); 

        return app; 
    }

    // Handlers 

    // GET Handlers

    private static Results<Ok<List<OnsiteCourse>>, ProblemHttpResult> GetAllOnsiteCourses(
        [FromServices] IOnsiteCoursesDatabaseService dbService)
    {
        List<OnsiteCourse> list = dbService.GetAllOnsiteCourse();

        return TypedResults.Ok(list);
    }

    private static Results<Ok<OnsiteCourse>, ProblemHttpResult> GetOnsiteCourseById(
        int id, [FromServices] IOnsiteCoursesDatabaseService dbService)
    {
        OnsiteCourse? course = dbService.GetOnsiteCourseByCourseId(id);

        if (course == null)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Course with id {id} does not exist");
        }

        return TypedResults.Ok(course);
    }


    // POST Handlers 

    private static Results<Created<OnsiteCourse>, ProblemHttpResult> CreateOnsiteCourse(
        OnsiteCourse onsiteCourse, [FromServices] IOnsiteCoursesDatabaseService dbService,
        [FromServices] LinkGenerator linkGenerator)
    {
        if (!dbService.CourseExists(onsiteCourse.CourseId))
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Can not create Onsite details for course that does not exists");
        }

        OnsiteCourse? created = dbService.CreateOnsiteCourse(onsiteCourse); 

        if (created == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to create onsite course"); 
        }

        string? link = linkGenerator.GetPathByName("OnsiteCourseById", new { id = created.CourseId }); 

        return TypedResults.Created(link, created);
    }


    // PUT Handlers 

    private static Results<NoContent, ProblemHttpResult> UpdateOnsiteCourseByID(
        int id, OnsiteCourse course, [FromServices] IOnsiteCoursesDatabaseService dbService)
    {
        bool entityExists = dbService.GetOnsiteCourseByCourseId(id) != null; 

        if (!entityExists)
        {
             return TypedResults.Problem(statusCode: 400, detail: $"onsite course with id {id} does not exist");
        }

        OnsiteCourse? updatedCourse = dbService.UpdateOnsiteCourseByCourseID(id, course);

        if (updatedCourse == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to update onsite course"); 
        }

        return TypedResults.NoContent();
    }


    // DELETE Handlers 

    private static Results<NoContent, ProblemHttpResult> DeleteOnsiteCourseById(
        int id, [FromServices] IOnsiteCoursesDatabaseService dbService)
    {
        bool entityExists = dbService.GetOnsiteCourseByCourseId(id) != null;

        if (!entityExists)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"onsite course with id {id} does not exist");
        }

        bool deleted = dbService.DeleteOnsiteCourseByCourseID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: $"Failed to process given request");
        }

        return TypedResults.NoContent();
    }
}
