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
        endpointsWithValidation.MapGet("/{id:int}", GetDepartmentByID); 
        endpoints.MapGet("/{name}", GetDepartmentByName);

        // POST Endpoints 
        endpoints.MapPost("/", CreateDepartment);

        // PUT Endpoints 
        endpointsWithValidation.MapPut("/{id:int}", UpdateDepartmentByID);

        // DELETE Endpoints 
        endpointsWithValidation.MapDelete("/{id:int}", DeleteDepartmentByID);

        return app; 
    }

    // Endpoints Handlers 

    // GET Handlers 
    
    private static Results<Ok<List<Department>>, ProblemHttpResult> GetAllDepartments(
        [FromServices] IDepartmentDatabaseService databaseService)
    {
        List<Department> departments = databaseService.GetAllDepartments(courses: true);

        return TypedResults.Ok(departments);
    }

    private static Results<Ok<Department>, ProblemHttpResult> GetDepartmentByID(
        int id, [FromServices] IDepartmentDatabaseService databaseService)
    {
        Department? department = databaseService.GetDepartmentById(id);

        if (department == null)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Departmnet with ID {id} does not exists"); 
        }

        return TypedResults.Ok(department);
    }

    private static Results<Ok<Department>, ProblemHttpResult> GetDepartmentByName(
        string name, [FromServices] IDepartmentDatabaseService databaseService)
    {
        Department? department = databaseService.GetDepartmentByName(name);

        if (department == null)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Departmnet with name {name} does not exists");
        }

        return TypedResults.Ok(department);
    }


    // POST Handlers 

    private static Results<Created<Department>, ProblemHttpResult> CreateDepartment(
        Department newDepartment, [FromServices] IDepartmentDatabaseService databaseService)
    {
        Department? createdDepartment = databaseService.CreateDepartment(newDepartment);

        if (createdDepartment == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to create department"); 
        }

        // implement link generator
        return TypedResults.Created($"department/{createdDepartment.DepartmentId}", createdDepartment); 
    }


    // PUT Handlers 

    private static Results<NoContent, ProblemHttpResult> UpdateDepartmentByID(
        int id, Department department, [FromServices] IDepartmentDatabaseService databaseService)
    {
        bool entityExists = databaseService.GetDepartmentById(id) != null;

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
        bool entityExists = databaseService.GetDepartmentById(id) != null;

        if (!entityExists)
        {
            return TypedResults.Problem(statusCode: 400, detail: $"Department with ID {id} does not exists");
        }

        bool deleted = databaseService.DeleteDepartmentByID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: $"Failed to delete department with id {id}"); 
        }

        return TypedResults.NoContent();
    }


}
