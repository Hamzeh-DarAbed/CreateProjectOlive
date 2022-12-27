using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using CreateProjectOlive.Context;
using Microsoft.EntityFrameworkCore;


namespace CreateProjectOlive.Test
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.test.json");

            builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<EF_DbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<EF_DbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest223");
                    options.UseInternalServiceProvider(serviceProvider);

                });


                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<EF_DbContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw ex;
                    }
                }
            });
        }
    }
}