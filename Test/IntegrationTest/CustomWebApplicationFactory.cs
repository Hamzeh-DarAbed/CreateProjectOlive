using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using CreateProjectOlive.Models;
using MongoOlive.DBContext;

namespace CreateProjectOlive.Test
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var projectDir = Directory.GetCurrentDirectory();

           builder.UseContentRoot(projectDir);

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDBContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<ApplicationDBContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDataBase");
                    options.UseInternalServiceProvider(serviceProvider);

                });


                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                        Seed(appContext);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            });
        }

        public virtual void Seed(ApplicationDBContext _context)
        {
            _context.Projects.Add(new Project
            {
                Id = Guid.Parse("85d05ebb-6cfc-435a-a7c6-ae92a553431b"),
                ProjectName = "Test Project",
                ProjectDescription = "Test Project Description",
                BusinessType = "Test Business Type",
                CreatedBy = "Test Created By",
                Domain = "Test Domain",
            });

            _context.Projects.Add(new Project
            {
                Id = Guid.Parse("85d05ebb-6cfc-435a-a7c6-ae92a553431c"),
                ProjectName = "Test Project 2",
                ProjectDescription = "Test Project Description 2",
                BusinessType = "Test Business Type 2",
                CreatedBy = "Test Created By 2",
                Domain = "Test Domain 2",
            });

            _context.SaveChanges();
        }
    }
}