using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using CreateProjectOlive.Context;
using Microsoft.EntityFrameworkCore;


namespace CreateProjectOlive.Test
{
    public class TestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        public HttpClient _httpClient;
        public WebApplicationFactory<Program> _factory;


        public TestBase(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.WithWebHostBuilder(host =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "appsettings.test.json");

                host.ConfigureAppConfiguration((context, conf) =>
                    {
                        conf.AddJsonFile(configPath);
                    });

                host.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                    typeof(DbContextOptions<EF_DbContext>));
                    services.Remove(descriptor!);

                    services.AddDbContext<EF_DbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDB");
                    });


                });
            }).CreateClient();
        }
    }
}