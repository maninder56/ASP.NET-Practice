using BasicFruitsAPI.Model;
using System.Runtime.CompilerServices;
using BasicFruitsAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using BasicFruitsAPI.ValidationFilters;

namespace BasicFruitsAPI;

public static class FuitsEndpointExtentionMethod 
{
    private static readonly FruitsService fruitsService = new FruitsService();

    private static IdValidationFilter _idValidationFilter = new IdValidationFilter(fruitsService);

    public static WebApplication AddFuitsEndpoints(this WebApplication app)
    {
        RouteGroupBuilder fruitApi = app.MapGroup("/fruit");

        RouteGroupBuilder fruitApiWithIdValidation = fruitApi.MapGroup("/")
            .AddEndpointFilter(_idValidationFilter); 

        // Get Endpoints
        fruitApi.MapGet("/list", GetFruitList);
        fruitApiWithIdValidation.MapGet("/id/{id:int}", GetFruitByID).WithName("fruitId"); 
        fruitApi.MapGet("/name/{name:alpha}", GetFruitByName);
        fruitApi.MapGet("/classification/{classification:alpha}", GetFruitsByClassification);
        fruitApi.MapGet("/available-classification", GetAvailableClassifications); 

        // Post Endpoints
        fruitApi.MapPost("/", CreateFruit);

        // Put Endpoints 
        fruitApiWithIdValidation.MapPut("/id/{id:int}", UpdateFruitByID);

        // Delete Endpoints 
        fruitApiWithIdValidation.MapDelete("/id/{id:int}", DeleteFruitByID);

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

    private static Results<Ok<Fruit>, ProblemHttpResult> GetFruitByID([FromRoute]int id)
    {
        Fruit? fruit = fruitsService.GetFruitByID(id);

        if (fruit == null)
        {
            return TypedResults.Problem(statusCode: 500);
        }

        return TypedResults.Ok(fruit);  
    }

    private static Results<Ok<Fruit>, ProblemHttpResult> GetFruitByName([FromRoute] string name)
    {
        Fruit? fruit = fruitsService.GetFruitByName(name);

        if (fruit == null)
        {
            return TypedResults.Problem(statusCode: 404); 
        }

        return TypedResults.Ok(fruit);
    }

    private static Results<Ok<List<Fruit>>, ProblemHttpResult> GetFruitsByClassification(
        [FromRoute] string classification)
    {
        List<Fruit>? fruitList = fruitsService.GetFruitsByClassification(classification);

        if (fruitList == null || fruitList.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404);
        }

        return TypedResults.Ok(fruitList);
    }

    private static Results<Ok<List<string>>, ProblemHttpResult> GetAvailableClassifications()
    {
        List<string> availableClassifications = fruitsService.GetAvailableClassifications();

        if (availableClassifications.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404); 
        }

        return TypedResults.Ok(availableClassifications);
    }

    

    // POST Handlers

    private static Results<Created<Fruit>, InternalServerError<string>> CreateFruit(
        [FromBody] Fruit newFruit, [FromServices] LinkGenerator link)
    {
        Fruit? createdFruit = fruitsService.CreateFruit(newFruit);

        if (createdFruit == null)
        {
            return TypedResults.InternalServerError("Failed to Create fruit");
        }

        string? createdFruitLink = link.GetPathByName("fruitId", new { id = createdFruit.Id }); 
        return TypedResults.Created(createdFruitLink,createdFruit);
    }


    // PUT Handlers 

    private static Results<NoContent, InternalServerError<string>> UpdateFruitByID(
        int  id, [FromBody] Fruit newFruit)
    {
        Fruit? updatedFruit = fruitsService.UpdateFruitByID(id, newFruit);

        if (updatedFruit == null)
        {
            return TypedResults.InternalServerError("Failed to Update fruit");
        }

        return TypedResults.NoContent(); 
    }


    // DELETE Handlers 

    private static Results<NoContent, InternalServerError<string>> DeleteFruitByID(int id)
    {
        if (!fruitsService.DeleteFruitByID(id))
        {
            return TypedResults.InternalServerError("Failed To Delete Fruit"); 
        }

        return TypedResults.NoContent();    
    }

}
