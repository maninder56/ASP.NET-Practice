using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Modles.DepartmentModels;
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
    
    private static Results<Ok<List<DepartmentModel>>, ProblemHttpResult> GetAllDepartments(
        [FromServices] IDepartmentDatabaseService database)
    {
        List<DepartmentModel> departments = database.GetAllDepartments();

        return TypedResults.Ok(departments);
    }

    private static Results<Ok<DepartmentModel>, ProblemHttpResult> GetDepartmentByID(
        int id, [FromServices] IDepartmentDatabaseService database)
    {
        DepartmentModel? department = database.GetDepartmentById(id);

        if (department == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: $"Departmnet with ID {id} does not exists"); 
        }

        return TypedResults.Ok(department);
    }

    private static Results<Ok<DepartmentModel>, ProblemHttpResult> GetDepartmentByName(
        string name, [FromServices] IDepartmentDatabaseService database)
    {
        DepartmentModel? department = database.GetDepartmentByName(name);

        if (department == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: $"Departmnet with name {name} does not exists");
        }

        return TypedResults.Ok(department);
    }


    // POST Handlers 

    private static Results<Created<DepartmentModel>, ProblemHttpResult> CreateDepartment(
        DepartmentModel newDepartment, [FromServices] IDepartmentDatabaseService database)
    {
        DepartmentModel? createdDepartment = database.CreateDepartment(newDepartment);

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
        int id, DepartmentModel department, [FromServices] IDepartmentDatabaseService database)
    {
        DepartmentModel? updatedDepartment = database.UpdateDepartmentByID(id, department);

        if (updatedDepartment == null)
        {
            return TypedResults.Problem(statusCode: 500, detail: "Failed to updated department"); 
        }

        return TypedResults.NoContent();
    }


    // DELETE Handlers 

    private static Results<NoContent, ProblemHttpResult> DeleteDepartmentByID(
        int id, [FromServices] IDepartmentDatabaseService database)
    {
        bool deleted = database.DeleteDepartmentByID(id);

        if (!deleted)
        {
            return TypedResults.Problem(statusCode: 500, detail: $"Failed to delete department with id {id}"); 
        }

        return TypedResults.NoContent();
    }


}
