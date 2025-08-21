using CommunityBoardAPI.Data; 

namespace CommunityBoardAPI.Services;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPosterServices(this IServiceCollection services)
    {
        services.AddSingleton(new PosterData());

        services.AddScoped<IPosterService, PosterService>(); 

        return services; 
    }
}
