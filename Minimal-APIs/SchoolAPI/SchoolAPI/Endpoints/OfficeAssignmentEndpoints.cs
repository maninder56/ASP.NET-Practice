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


    // POST Handlers 
    // PUT Handlers 
    // DELETE Handlers 
}
