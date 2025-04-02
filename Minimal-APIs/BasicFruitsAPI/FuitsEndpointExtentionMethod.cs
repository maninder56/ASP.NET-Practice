using BasicFruitsAPI.Model;
using System.Runtime.CompilerServices;
using BasicFruitsAPI.Services; 

namespace BasicFruitsAPI;

public static class FuitsEndpointExtentionMethod 
{
    private static readonly FruitsService fruitsService = new FruitsService();

    public static WebApplication AddFuitsEndpoints(this WebApplication app)
    {
        // Get Endpoints
        app.MapGet("/fruitlist", fruitsService.GetFruitList);
        app.MapGet("/fruitbyid/{id:int}", fruitsService.GetFruitByID);
        app.MapGet("/fruitbyname/{name}", fruitsService.GetFruitByName);
        app.MapGet("/fruitbyclassification/{classification}", fruitsService.GetFruitsByClassification); 

        // Post Endpoints
        app.MapPost("/fruit", fruitsService.CreateFruit);

        // Put Endpoints 
        app.MapPut("/fruit/{id:int}", fruitsService.UpdateFruitByID);

        // Delete Endpoints 
        app.MapDelete("/fruit/{id:int}", fruitsService.DeleteFruitByID); 

        return app; 
    }
}
