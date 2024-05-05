using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestingWithDb.Infrastructure;
using TestingWithDb.Migrations;

namespace TestingWithDb.Api.Startup;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, ConfigurationManager configuration,
        IWebHostEnvironment environment)
    {
        services.AddEntityFrameworkMySql();
        services.AddEntityFrameworkNamingConventions();
        var migrationAssemblyName = Assembly.GetAssembly(typeof(PlaceHolderForAssemblyReference))!.GetName().Name;

        services.AddDbContextPool<ProductDbContext>((servicesProvider, dbOptions) =>
        {
            var connectionString = configuration.GetConnectionString("Db");
            dbOptions
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), o =>
                {
                    o.MigrationsAssembly(migrationAssemblyName);
                    o.MigrationsHistoryTable($"__{nameof(ProductDbContext)}");
                })
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging(configuration.GetValue<bool>("EnableDbSensitiveDataLogging"))
                .UseSnakeCaseNamingConvention()
                .UseInternalServiceProvider(servicesProvider);
        });

        return services;
    }
}