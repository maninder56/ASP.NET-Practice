using BasicFruitsAPI.Model;
using System.Runtime.CompilerServices;
using BasicFruitsAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using BasicFruitsAPI.ValidationFilters;

namespace BasicFruitsAPI;

public static class FuitsEndpointExtensionMethod 
{
    public static WebApplication AddFuitsEndpoints(this WebApplication app)
    {
        RouteGroupBuilder fruitApi = app.MapGroup("/fruit");

        RouteGroupBuilder fruitApiWithIdValidation = fruitApi.MapGroup("/")
            .AddEndpointFilter<IdValidationFilter>();

        // Get Endpoints
        fruitApi.MapGet("/list", GetFruitList)
            .WithName("GetFruitList")
            .WithTags("fruit")
            .Produces<List<Fruit>>()
            .ProducesProblem(404); 

        fruitApiWithIdValidation.MapGet("/id/{id:int}", GetFruitByID)
            .WithName("GetFruitByID")
            .WithTags("fruit")
            .Produces<Fruit>()
            .ProducesValidationProblem(); 

        fruitApi.MapGet("/name/{name}", GetFruitByName)
            .WithName("GetFruitByName")
            .WithTags("fruit")
            .Produces<Fruit>()
            .ProducesProblem(404);

        fruitApi.MapGet("/classification/{classification:alpha}", GetFruitsByClassification)
            .WithName("GetFruitsByClassification")
            .WithTags("fruit")
            .Produces<List<Fruit>>()
            .ProducesProblem(404);

        fruitApi.MapGet("/available-classification", GetAvailableClassifications)
            .WithName("GetAvailableClassifications")
            .WithTags("fruit")
            .Produces<List<string>>()
            .ProducesProblem(404);


        // Post Endpoints
        fruitApi.MapPost("/", CreateFruit)
            .WithName("CreateFruit")
            .WithParameterValidation()
            .WithTags("fruit")
            .Produces<Fruit>(201)
            .ProducesValidationProblem();


        // Put Endpoints 
        fruitApiWithIdValidation.MapPut("/id/{id:int}", UpdateFruitByID)
            .WithName("UpdateFruitByID")
            .WithParameterValidation()
            .WithTags("fruit")
            .Produces(204)
            .ProducesValidationProblem()
            .ProducesProblem(500);


        // Delete Endpoints 
        fruitApiWithIdValidation.MapDelete("/id/{id:int}", DeleteFruitByID)
            .WithName("DeleteFruitByID")
            .WithTags("fruit")
            .Produces(204)
            .ProducesValidationProblem()
            .ProducesProblem(500);

        return app; 
    }


    // Endpoint Handlers

    #region Get Handlers
    private static Results<Ok<List<Fruit>>, ProblemHttpResult> GetFruitList(
        [FromServices] IFruitService fruitService)
    {
        List<Fruit> fruitList = fruitService.GetFruitList();

        if (fruitList.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404); 
        }

        return TypedResults.Ok(fruitList);
    }

    private static Results<Ok<Fruit>, ProblemHttpResult> GetFruitByID(
        [FromRoute]int id, [FromServices] IFruitService fruitService)
    {
        Fruit? fruit = fruitService.GetFruitByID(id);

        if (fruit == null)
        {
            return TypedResults.Problem(statusCode: 500);
        }

        return TypedResults.Ok(fruit);  
    }

    private static Results<Ok<Fruit>, ProblemHttpResult> GetFruitByName(
        [FromRoute] string name, [FromServices] IFruitService fruitService)
    {
        Fruit? fruit = fruitService.GetFruitByName(name);

        if (fruit == null)
        {
            return TypedResults.Problem(statusCode: 404); 
        }

        return TypedResults.Ok(fruit);
    }

    private static Results<Ok<List<Fruit>>, ProblemHttpResult> GetFruitsByClassification(
        [FromRoute] string classification, [FromServices] IFruitService fruitService)
    {
        List<Fruit>? fruitList = fruitService.GetFruitsByClassification(classification);

        if (fruitList == null || fruitList.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404);
        }

        return TypedResults.Ok(fruitList);
    }

    private static Results<Ok<List<string>>, ProblemHttpResult> GetAvailableClassifications(
        [FromServices] IFruitService fruitService)
    {
        List<string> availableClassifications = fruitService.GetAvailableClassifications();

        if (availableClassifications.Count == 0)
        {
            return TypedResults.Problem(statusCode: 404); 
        }

        return TypedResults.Ok(availableClassifications);
    }

    #endregion


    #region POST Handlers

    private static Results<Created<Fruit>, InternalServerError<string>> CreateFruit(
        [FromBody] Fruit newFruit, [FromServices] LinkGenerator link, [FromServices] IFruitService fruitService)
    {
        Fruit? createdFruit = fruitService.CreateFruit(newFruit);

        if (createdFruit == null)
        {
            return TypedResults.InternalServerError("Failed to Create fruit");
        }

        string? createdFruitLink = link.GetPathByName("GetFruitByID", new { id = createdFruit.Id }); 
        return TypedResults.Created(createdFruitLink,createdFruit);
    }
    #endregion

    #region PUT Handlers 

    private static Results<NoContent, InternalServerError<string>> UpdateFruitByID(
        int  id, [FromBody] Fruit newFruit, [FromServices] IFruitService fruitService)
    {
        Fruit? updatedFruit = fruitService.UpdateFruitByID(id, newFruit);

        if (updatedFruit == null)
        {
            return TypedResults.InternalServerError("Failed to Update fruit");
        }

        return TypedResults.NoContent(); 
    }
    #endregion

    #region DELETE Handlers 

    private static Results<NoContent, InternalServerError<string>> DeleteFruitByID(
        int id, [FromServices] IFruitService fruitService)
    {
        if (!fruitService.DeleteFruitByID(id))
        {
            return TypedResults.InternalServerError("Failed To Delete Fruit"); 
        }

        return TypedResults.NoContent();    
    }
    #endregion
}
