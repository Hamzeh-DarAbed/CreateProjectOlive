using Microsoft.AspNetCore.Mvc.Testing;
using CreateProjectOlive.Context;
using Microsoft.EntityFrameworkCore;
using CreateProjectOlive.Models;
using Microsoft.AspNetCore.Identity;

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
                    options.UseInMemoryDatabase("InMemoryDataBase");
                    options.UseInternalServiceProvider(serviceProvider);
                });


                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<EF_DbContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                        Seed(appContext);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Seed Data Failed", ex);
                    }
                }
            });
        }

        public virtual async void Seed(EF_DbContext _context)
        {
            var user =

                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "AdminTest",
                    NormalizedUserName = "ADMINTEST",
                    Email = "adminTest@adminTest.com",
                    NormalizedEmail = "ADMINTEST@ADMINTEST.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI99MKFTBxu+rmOvvm8PZbXbVHCLtDa57L+/ZS/68kKU6e0OcP5Sg+U6+TYJ3s1yFg==",
                    SecurityStamp = "P4HECIURGID4GGDNIUF24PQTFTTYDCER"
                };

            var SuperAdminRole = _context.Roles.Where(a => a.Name == "SuperAdmin").FirstOrDefault();


            var identityUserRole = new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = SuperAdminRole!.Id

            };

            await _context.Users.AddAsync(user);
            await _context.UserRoles.AddAsync(identityUserRole);

            _context.SaveChanges();
        }
    }
}