using MongoDB.Driver;
using VacancyAnalyzerHH.Contracts.Interfaces;
using VacancyAnalyzerHH.Core.Repositories;
using VacancyAnalyzerHH.Infrastructure.Services;
using VacancyAnalyzerHH.MongoDb;
using VacancyAnalyzerHH.MongoDb.Implementations;

namespace VacancyAnalyzerHH.Api.Code;

public static class IoC
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyMarker).Assembly)
            );

        // Logger
        services.AddScoped<IHeadHunterClient, HeadHunterClient>();
        services.AddScoped<IVacancyRepository, VacancyRepository>();

        services.AddSingleton<IMongoClient>(new MongoClient(
            configuration.GetSection("MongoDbSettings:ConnectionStrings")
            .Value));

        services.AddSingleton<MongoDbContext>();

        return services;
    }
}
