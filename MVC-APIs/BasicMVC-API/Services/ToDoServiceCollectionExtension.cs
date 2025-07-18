using BasicMVC_API.Data;

namespace BasicMVC_API.Services; 

public static class ToDoServiceCollectionExtension
{
    public static IServiceCollection AddToDoServices(this IServiceCollection services)
    {
        services.AddSingleton<ToDoData>(); 

        services.AddScoped<IToDoService, ToDoService>();

        return services; 
    }
}
