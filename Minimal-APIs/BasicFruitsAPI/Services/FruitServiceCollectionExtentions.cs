namespace BasicFruitsAPI.Services; 

public static class FruitServiceCollectionExtentions
{
    public static IServiceCollection AddFruitService(this IServiceCollection services)
    {
        services.AddSingleton<IFruitService, FruitsService>();

        return services; 
    }
}
