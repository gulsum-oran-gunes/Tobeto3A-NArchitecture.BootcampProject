using Application.Services.BootcampVideoService;
using Application.Services.ImageService;
using Application.Services.VideoService;
using Google.Apis.YouTube.v3;
using Infrastructure.Adapters.ImageService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        services.AddScoped<IBootcampVideoService, BootcampVideoManager>();
        //services.AddScoped<VideoServiceBase, YouTubeService>();





        return services;
    }
}
