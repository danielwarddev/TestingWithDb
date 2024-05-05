using TestingWithDb.Infrastructure.Repositories;

namespace TestingWithDb.Api.Startup;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration,
        IHostEnvironment environment)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}