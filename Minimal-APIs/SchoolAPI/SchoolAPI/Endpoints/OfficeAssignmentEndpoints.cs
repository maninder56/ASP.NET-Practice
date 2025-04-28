using DatabaseContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SchoolAPI.EndpointFilters;
using SchoolAPI.Services;

namespace SchoolAPI.Endpoints; 

public static class OfficeAssignmentEndpoints
{
    public static WebApplication MapOfficeAssignmentEndpoints(this WebApplication app)
    {
        RouteGroupBuilder endpoint = app.MapGroup("/office-assignment");

        RouteGroupBuilder endpointWithValidaiton = endpoint.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>()
            .AddEndpointFilter<OfficeAssignmentExistsValidationFilter>();

        // GET Endpoints 
        endpoint.MapGet("/", GetAllOfficeAssignments); 
        endpointWithValidaiton.MapGet("/{id:int}", GetOfficeAssignmentByID)
            .WithName("GetOfficeAssignmentByID");

        // POST Endpoints 
        endpoint.MapPost("/", CreateOfficeAssignment)
            .WithParameterValidation();

        // PUT Endpoints 
        endpointWithValidaiton.MapPut("/{id:int}", UpdateOfficeAssignmentByID)
            .WithParameterValidation();

        // DELETE Endpoints 
        endpointWithValidaiton.MapDelete("/{id:int}", DeleteOfficeAssignmentByID);

        return app; 
    }

    // Handlers 

    // Get Handlers 
    private static Results<Ok<List<OfficeAssignment>>, ProblemHttpResult> GetAllOfficeAssignments(
        [FromServices] IOfficeAssignmentDatabaseService dbService)
    {
        List<OfficeAssignment> officeAssignments = dbService.GetAllOfficeAssignments();

        return TypedResults.Ok(officeAssignments);
    }

    private static Results<Ok<OfficeAssignment>,ProblemHttpResult> GetOfficeAssignmentByID(
        int id, [FromServices] IOfficeAssignmentDatabaseService dbService)
    {
        OfficeAssignment? officeAssignment = dbService.GetOfficeAssignmentById(id);

        if (officeAssignment == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to retrive Office Assignment");
        }

        return TypedResults.Ok(officeAssignment);
    }


    // POST Handlers 
    private static Results<Created<OfficeAssignment>, ProblemHttpResult, ValidationProblem> CreateOfficeAssignment(
        OfficeAssignment officeAssignment, [FromServices] IOfficeAssignmentDatabaseService dbService, 
        [FromServices] LinkGenerator linkGenerator)
    {
        bool instructorAlreadyAssigned = dbService.OfficeAssignmentExists(officeAssignment.InstructorId);

        if (instructorAlreadyAssigned)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                { "InstructorID", new string[] { $"Instructor with ID {officeAssignment.InstructorId} has already been assigned an office" } }
            });
        }

        bool instructorExists = dbService.InstructorExists(officeAssignment.InstructorId);

        if (!instructorExists)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                { "InstructorID", new string[] { $"Can not Assign office to Instructor with ID {officeAssignment.InstructorId} which does not exists" } }
            }); 
        }

        OfficeAssignment? created = dbService.CreateOffiAssignment(officeAssignment);

        if (created == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to Assign Office"); 
        }

        string? link = linkGenerator.GetPathByName("GetOfficeAssignmentByID", new { created.InstructorId }); 

        return TypedResults.Created(link, created);
    }


    // PUT Handlers 
    private static Results<NoContent, ProblemHttpResult> UpdateOfficeAssignmentByID(
        int id, OfficeAssignment officeAssignment, [FromServices] IOfficeAssignmentDatabaseService dbService)
    {
        bool updated = dbService.UpdateOfficeAssignmentByID(id, officeAssignment);

        if (!updated)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to update office assignemtn"); 
        }

        return TypedResults.NoContent(); 
    }

    // DELETE Handlers 
    private static Results<NoContent, ProblemHttpResult> DeleteOfficeAssignmentByID(
        int id, [FromServices] IOfficeAssignmentDatabaseService dbService)
    {
        bool deleted = dbService.DeleteOfficeAssignmentByID(id); 

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to delete office assignemtn");
        }

        return TypedResults.NoContent();
    }
}
