using ToDoWebApp.Data;

namespace ToDoWebApp.Services; 

public static class TodoServiceCollectionExtension
{
    public static IServiceCollection AddToDoServices(this IServiceCollection service)
    {
        service.AddSingleton<ToDoData>();

        service.AddScoped<IToDoService, ToDoService>(); 

        return service; 
    }
}
