namespace NotesWebApp.Services; 

public static class NoteServiceCollectionExtension
{
    public static IServiceCollection AddNotesServices(this IServiceCollection services)
    {
        services.AddSingleton<NotesData>(); 

        services.AddScoped<INotesDataService, NotesDataService>();

        return services;
    }
}
