namespace ShoppingList.Services; 

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddShoppingService(this IServiceCollection service)
    {
        service.AddSingleton<ShoppingListData>(); 

        service.AddScoped<IShoppingListService, ShoppingListService>();

        return service; 
    }
}
