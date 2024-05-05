using System.Reflection;
using TestingWithDb.Api.Startup;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            
            var builder = WebApplication.CreateBuilder(args);
            var assembly = Assembly.GetExecutingAssembly();

            // Add services to the container.

            builder.Services.AddControllers();
            var configuration = GetConfiguration();
            builder.Configuration.AddConfiguration(configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContexts(builder.Configuration, builder.Environment);
            builder.Services.AddServices(builder.Configuration, builder.Environment);
            builder.Services.AddHealthChecks();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");
            app.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();

        var config = builder.Build();
        return config;
    }
}