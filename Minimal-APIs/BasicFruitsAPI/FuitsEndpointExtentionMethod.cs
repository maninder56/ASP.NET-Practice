using BasicFruitsAPI.Model;
using System.Runtime.CompilerServices;
using BasicFruitsAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

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

    private static Results<Ok<List<Fruit>>, NotFound> GetFruitList()
    {
        List<Fruit> fruitList = fruitsService.GetFruitList();

        if (fruitList.Count == 0)
        {
            return TypedResults.NotFound(); 
        }

        return TypedResults.Ok(fruitList);
    }

    private static Results<Ok<Fruit>, NotFound> GetFruitByID(int id)
    {
        Fruit? fruit = fruitsService.GetFruitByID(id);

        if (fruit == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(fruit);  
    }

    private static Results<Ok<Fruit>, NotFound> GetFruitByName(string name)
    {
        Fruit? fruit = fruitsService.GetFruitByName(name);

        if (fruit == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(fruit);
    }

    private static Results<Ok<List<Fruit>>, NotFound> GetFruitsByClassification(string classification)
    {
        List<Fruit>? fruitList = fruitsService.GetFruitsByClassification(classification);

        if (fruitList == null || fruitList.Count == 0)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(fruitList);
    }

    

    // POST Handlers

    private static Results<Created<Fruit>, BadRequest<string>> CreateFruit(Fruit newFruit)
    {
        Fruit? createdFruit = fruitsService.CreateFruit(newFruit);

        if (createdFruit == null)
        {
            return TypedResults.BadRequest("Duplicate names are not allowed.");
        }

        return TypedResults.Created($"/fruitbyid/{createdFruit.Id}",createdFruit);
    }


    // PUT Handlers 

    private static Results<NoContent, BadRequest<string>, InternalServerError<string>> UpdateFruitByID(int  id, Fruit newFruit)
    {

        if (id < 1 || !(fruitsService.isIDValid(id)))
        {
            return TypedResults.BadRequest("ID is not valid");
        }

        Fruit? updatedFruit = fruitsService.UpdateFruitByID(id, newFruit);

        if (updatedFruit == null)
        {
            return TypedResults.InternalServerError("Failed to Update fruit");
        }

        return TypedResults.NoContent(); 
    }


    // DELETE Handlers 

    private static Results<NoContent, BadRequest<string>, InternalServerError<string>> DeleteFruitByID(int id)
    {
        if (id < 1 || !(fruitsService.isIDValid(id)))
        {
            return TypedResults.BadRequest("ID is not valid"); 
        }

        if (!fruitsService.DeleteFruitByID(id))
        {
            return TypedResults.InternalServerError("Failed To Delete Fruit"); 
        }

        return TypedResults.NoContent();    
    }

}
