using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DatabaseContext; 
using SchoolAPI.Services;
using SchoolAPI.EndpointFilters;

namespace SchoolAPI.Endpoints; 

public static class DepartmentEndpoints
{
    public static WebApplication MapDepartmentEndpoints(this WebApplication app)
    {
        RouteGroupBuilder endpoints = app.MapGroup("/departmnet");

        RouteGroupBuilder endpointsWithValidation = endpoints.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>(); 


        // GET Endpoints
        endpoints.MapGet("/", GetAllDepartments);
        endpointsWithValidation.MapGet("/{id:int}", GetDepartmentByID)
            .WithName("DepartmentByID"); 
        endpoints.MapGet("/{name}", GetDepartmentByName);

        // POST Endpoints 
        endpoints.MapPost("/", CreateDepartment)
            .WithParameterValidation();

        // PUT Endpoints 
        endpointsWithValidation.MapPut("/{id:int}", UpdateDepartmentByID)
            .WithParameterValidation();

        // DELETE Endpoints 
        endpointsWithValidation.MapDelete("/{id:int}", DeleteDepartmentByID);

        return app; 
    }

    // Endpoints Handlers 

    // GET Handlers 
    
    private static Results<Ok<List<Department>>, ProblemHttpResult> GetAllDepartments(
        [FromQuery] bool? courses, [FromServices] IDepartmentDatabaseService databaseService)
    {
        List<Department> departments = databaseService.GetAllDepartments(courses ?? false);

        return TypedResults.Ok(departments);
    }

    private static Results<Ok<Department>, ProblemHttpResult> GetDepartmentByID(
        int id, [FromQuery] bool? courses, [FromServices] IDepartmentDatabaseService databaseService)
    {
        Department? department = databaseService.GetDepartmentById(id, courses ?? false);

        if (department == null)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Departmnet with ID {id} does not exists"); 
        }

        return TypedResults.Ok(department);
    }

    private static Results<Ok<Department>, ProblemHttpResult> GetDepartmentByName(
        string name, [FromQuery] bool? courses, [FromServices] IDepartmentDatabaseService databaseService)
    {
        Department? department = databaseService.GetDepartmentByName(name, courses ?? false);

        if (department == null)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Departmnet with name {name} does not exists");
        }

        return TypedResults.Ok(department);
    }


    // POST Handlers 

    private static Results<Created<Department>, ProblemHttpResult> CreateDepartment(
        Department newDepartment, [FromServices] IDepartmentDatabaseService databaseService,
        [FromServices] LinkGenerator linkGenerator)
    {
        Department? createdDepartment = databaseService.CreateDepartment(newDepartment);

        if (createdDepartment == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to create department"); 
        }

        string? link = linkGenerator.GetPathByName("DepartmentByID", new { id = createdDepartment.DepartmentId }); 

        return TypedResults.Created(link, createdDepartment); 
    }


    // PUT Handlers 

    private static Results<NoContent, ProblemHttpResult> UpdateDepartmentByID(
        int id, Department department, [FromServices] IDepartmentDatabaseService databaseService)
    {
        bool entityExists = databaseService.GetDepartmentById(id, false) != null;

        if (!entityExists)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Department with ID {id} does not exists"); 
        }

        Department? updatedDepartment = databaseService.UpdateDepartmentByID(id, department);

        if (updatedDepartment == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to updated department"); 
        }

        return TypedResults.NoContent();
    }


    // DELETE Handlers 

    private static Results<NoContent, ProblemHttpResult> DeleteDepartmentByID(
        int id, [FromServices] IDepartmentDatabaseService databaseService)
    {
        bool deleted = databaseService.DeleteDepartmentByID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Department with ID {id} does not exists");
        }

        return TypedResults.NoContent();
    }


}
