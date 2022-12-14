using CreateProjectOlive.Models;
using CreateProjectOlive.Services;
using CreateProjectOlive.UnitOfWork;

public class Startup
{
    public WebApplication InitializeApplication()
    {
        var builder = WebApplication.CreateBuilder();
        ConfigureServices(builder.Services);
        var app = builder.Build();
        Configure(app, app.Environment);
        return app;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSingleton(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddSingleton(typeof(IProjectService), typeof(ProjectService));

        services.Configure<ProjectDataBaseConfig>(config.GetSection("MongoDb"));
        services.AddSwaggerGen();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}