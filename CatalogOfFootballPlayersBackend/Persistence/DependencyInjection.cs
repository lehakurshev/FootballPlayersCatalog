
using System.Reflection;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<CatalogOfFootballPlayersDbContext>(options => { options.UseNpgsql(connectionString); });

        
        services.AddScoped<IFootballPlayerRepository>(provider =>
            provider.GetService<FootballPlayerRepository>());
        
        services.AddScoped<ITeamRepository>(provider =>
            provider.GetService<TeamRepository>());
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        return services;
    }
}