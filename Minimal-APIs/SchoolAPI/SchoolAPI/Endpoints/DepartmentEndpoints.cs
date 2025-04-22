using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DatabaseContext; 
using SchoolAPI.Services;

namespace SchoolAPI.Endpoints; 

public static class DepartmentEndpoints
{
    public static WebApplication MapDepartmentEndpoints(this WebApplication app)
    {
        RouteGroupBuilder departmnetEndpoints = app.MapGroup("/departmnet");

        // GET Endpoints
        departmnetEndpoints.MapGet("/", GetAllDepartments);
        departmnetEndpoints.MapGet("/{id:int}", GetDepartmentByID); 
        departmnetEndpoints.MapGet("/{name}", GetDepartmentByName);

        // POST Endpoints 
        departmnetEndpoints.MapPost("/", CreateDepartment);

        // PUT Endpoints 
        departmnetEndpoints.MapPut("/{id:int}", UpdateDepartmentByID);

        // DELETE Endpoints 
        departmnetEndpoints.MapDelete("/{id:int}", DeleteDepartmentByID);

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
            return TypedResults.Problem(statusCode: 404, detail: $"Departmnet with ID {id} does not exists"); 
        }

        return TypedResults.Ok(department);
    }

    private static Results<Ok<Department>, ProblemHttpResult> GetDepartmentByName(
        string name, [FromServices] IDepartmentDatabaseService databaseService)
    {
        Department? department = databaseService.GetDepartmentByName(name);

        if (department == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: $"Departmnet with name {name} does not exists");
        }

        return TypedResults.Ok(department);
    }


    // POST Handlers 

    private static Results<Created<Department>, ProblemHttpResult> CreateDepartment(
        Department newDepartment, [FromServices] IDepartmentDatabaseService databaseService)
    {
        Department? createdDepartment = databaseService.CreateDepartment(newDepartment);

        // return 400 status code when validation fails 

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
            return TypedResults.Problem(statusCode: 500, detail: $"Failed to delete department with id {id}"); 
        }

        return TypedResults.NoContent();
    }


}
