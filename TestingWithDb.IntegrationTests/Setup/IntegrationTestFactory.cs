using System.Data.Common;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.MySql;
using TestingWithDb.Database;

namespace TestingWithDb.IntegrationTests.Setup;

[CollectionDefinition(nameof(DatabaseTestCollection))]
public class DatabaseTestCollection : ICollectionFixture<IntegrationTestFactory>
{
}

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer _container  = new MySqlBuilder()
        .WithDatabase("db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithExposedPort(3306)
        .WithWaitStrategy(Wait.ForUnixContainer() .UntilPortIsAvailable(3306))
        .WithCleanUp(true)
        .Build();

    public ProductContext Db { get; private set; } = null!;
    private Respawner _respawner = null!;
    private DbConnection _connection = null!;

    public async Task ResetDatabase()
    {
        await _respawner.ResetAsync(_connection);
    }
 
    public async Task InitializeAsync()
    {
        try
        {
            await _container.StartAsync();
            await _container.ExecAsync(["mysql", "-p", "mysql", "-e", "ALTER DATABASE db CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;"]);

            Db = Services.CreateScope().ServiceProvider.GetRequiredService<ProductContext>();
            _connection = Db.Database.GetDbConnection();
            await _connection.OpenAsync();

            _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.MySql,
                SchemasToInclude = new[] { "db" }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public new async Task DisposeAsync()
    {
        await _connection.CloseAsync();
        await _container.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connString = _container.GetConnectionString();
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<ProductContext>();
            services.AddDbContext<ProductContext>(options =>
            {
                options.UseMySql(connString, ServerVersion.AutoDetect(connString));
            });
            services.EnsureDbCreated<ProductContext>();
        });
    }
}

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<T>));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }

    public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<T>();
        context.Database.EnsureCreated();
    }
}