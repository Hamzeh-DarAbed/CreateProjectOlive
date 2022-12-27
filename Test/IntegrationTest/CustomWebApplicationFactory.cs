using Microsoft.AspNetCore.Mvc.Testing;
using MongoOlive.Test.IntegrationTest;
using Microsoft.EntityFrameworkCore;
using CreateProjectOlive.Services;
using CreateProjectOlive.UnitOfWork;
using AutoMapper;

public class GenericWebApplicationFactory<TStartup, TContext, TSeed>
    : WebApplicationFactory<TStartup>
    where TStartup : class
    where TContext : DbContext
    where TSeed : class, ISeedDataClass
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment("Testing");
        builder.UseContentRoot(".");
        
        builder.ConfigureServices(services =>
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            services.AddScoped<ISeedDataClass, TSeed>();

            services.AddDbContext<TContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                
            });
            
            


            ServiceProvider sp = services.BuildServiceProvider();
            using (IServiceScope scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TContext>();
                var logger = scopedServices.GetRequiredService<ILogger<GenericWebApplicationFactory<TStartup, TContext, TSeed>>>();

                var seeder = scopedServices.GetRequiredService<ISeedDataClass>();

                db.Database.EnsureCreated();

                try
                {
                    seeder.InitializeDbForTests();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                }
            }
        });
    }
}