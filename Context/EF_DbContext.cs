using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CreateProjectOlive.Models;

namespace CreateProjectOlive.Context
{
    public class EF_DbContext : IdentityDbContext<User>
    {
        public EF_DbContext(DbContextOptions<EF_DbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@optimumpartners.com",
                    NormalizedEmail = "ADMIN@OPTIMUMPARTNERS.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI99MKFTBxu+rmOvvm8PZbXbVHCLtDa57L+/ZS/68kKU6e0OcP5Sg+U6+TYJ3s1yFg==",
                    SecurityStamp = "P4HECIURGID4GGDNIUF24PQTFTTYDCER"
                }
            );
        }

    }
}