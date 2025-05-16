namespace LoggingErrors.Services; 

public static class ProductServiceCollectionExtension
{
    public static IServiceCollection AddProductServices(this IServiceCollection services)
    {
        services.AddSingleton<ProductData>();

        services.AddScoped<IProductDataService, ProductDataService>(); 

        return services;
    }
}
