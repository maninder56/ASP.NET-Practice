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
        app.MapGet("/fruit", () => "Hello from Fuit class"); 
        app.MapGet("/fruitlist", () => fruitsService.GetFruitList()); 

        return app; 
    }
}
