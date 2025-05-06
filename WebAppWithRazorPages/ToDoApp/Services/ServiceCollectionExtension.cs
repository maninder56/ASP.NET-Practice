using ToDoApp.ToDoData;

namespace ToDoApp.Services; 

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTodoService(this IServiceCollection services)
    {
        services.AddSingleton<ToDoConcurrentDictionary>(); 

        services.AddScoped<IToDoDataService, ToDoDataService>();

        return services; 
    }
}
