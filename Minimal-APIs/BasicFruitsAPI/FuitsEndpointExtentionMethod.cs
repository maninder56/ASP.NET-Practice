using BasicFruitsAPI.Model;
using System.Runtime.CompilerServices;
using BasicFruitsAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BasicFruitsAPI;

public static class FuitsEndpointExtentionMethod 
{
    private static readonly FruitsService fruitsService = new FruitsService();

    public static WebApplication AddFuitsEndpoints(this WebApplication app)
    {
        // Get Endpoints
        app.MapGet("/fruitlist", GetFruitList);
        app.MapGet("/fruitbyid/{id:int}", GetFruitByID);
        app.MapGet("/fruitbyname/{name}", GetFruitByName);
        app.MapGet("/fruitbyclassification/{classification}", GetFruitsByClassification); 

        // Post Endpoints
        app.MapPost("/fruit", CreateFruit);

        // Put Endpoints 
        app.MapPut("/fruit/{id:int}", UpdateFruitByID);

        // Delete Endpoints 
        app.MapDelete("/fruit/{id:int}", DeleteFruitByID); 

        return app; 
    }


    // Endpoint Handlers


    // GET Handlers

    private static Results<Ok<List<Fruit>>, ProblemHttpResult> GetFruitList()
    {
        List<Fruit> fruitList = fruitsService.GetFruitList();

        if (fruitList.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404); 
        }

        return TypedResults.Ok(fruitList);
    }

    private static Results<Ok<Fruit>, ProblemHttpResult> GetFruitByID(int id)
    {
        Fruit? fruit = fruitsService.GetFruitByID(id);

        if (fruit == null)
        {
            return TypedResults.Problem(statusCode: 404, detail: $"Fruit with ID: {id} can not be found.");
        }

        return TypedResults.Ok(fruit);  
    }

    private static Results<Ok<Fruit>, ProblemHttpResult> GetFruitByName(string name)
    {
        Fruit? fruit = fruitsService.GetFruitByName(name);

        if (fruit == null)
        {
            return TypedResults.Problem(statusCode: 404); 
        }

        return TypedResults.Ok(fruit);
    }

    private static Results<Ok<List<Fruit>>, ProblemHttpResult> GetFruitsByClassification(string classification)
    {
        List<Fruit>? fruitList = fruitsService.GetFruitsByClassification(classification);

        if (fruitList == null || fruitList.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404);
        }

        return TypedResults.Ok(fruitList);
    }

    

    // POST Handlers

    private static Results<Created<Fruit>, ValidationProblem> CreateFruit(Fruit newFruit)
    {
        Fruit? createdFruit = fruitsService.CreateFruit(newFruit);

        if (createdFruit == null)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                {"name" , new[] { "Duplicate names are not allowed." } }
            });
        }

        return TypedResults.Created($"/fruitbyid/{createdFruit.Id}",createdFruit);
    }


    // PUT Handlers 

    private static Results<NoContent, ValidationProblem, InternalServerError<string>> UpdateFruitByID(int  id, Fruit newFruit)
    {

        if (id < 1 || !(fruitsService.isIDValid(id)))
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                {"id", new string[] { "Invalid ID" , $"Fruit with ID {id} does not exists." } }
            });
        }

        Fruit? updatedFruit = fruitsService.UpdateFruitByID(id, newFruit);

        if (updatedFruit == null)
        {
            return TypedResults.InternalServerError("Failed to Update fruit");
        }

        return TypedResults.NoContent(); 
    }


    // DELETE Handlers 

    private static Results<NoContent, ValidationProblem, InternalServerError<string>> DeleteFruitByID(int id)
    {
        if (id < 1 || !(fruitsService.isIDValid(id)))
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                {"id", new string[] { "Invalid ID", $"Fruit with ID {id} does not exists." } }
            });
        }

        if (!fruitsService.DeleteFruitByID(id))
        {
            return TypedResults.InternalServerError("Failed To Delete Fruit"); 
        }

        return TypedResults.NoContent();    
    }

}
