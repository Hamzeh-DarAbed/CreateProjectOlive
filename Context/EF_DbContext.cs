using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CreateProjectOlive.Models;
using Microsoft.AspNetCore.Identity;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MongoOlive.Models;

namespace CreateProjectOlive.Context
{
    public class EF_DbContext : IdentityDbContext<User>
    {

        public EF_DbContext(DbContextOptions<EF_DbContext> options) : base(options) { }


        private static User[] _users = new User[]
        {

            new User
            {
            Id = Guid.NewGuid().ToString(),
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@optimumpartners.com",
            NormalizedEmail = "ADMIN@OPTIMUMPARTNERS.COM",
            PasswordHash = "AQAAAAEAACcQAAAAEI99MKFTBxu+rmOvvm8PZbXbVHCLtDa57L+/ZS/68kKU6e0OcP5Sg+U6+TYJ3s1yFg==",
            SecurityStamp = "P4HECIURGID4GGDNIUF24PQTFTTYDCER"
            }

        };
        private static IdentityRole[] _roles = new IdentityRole[]
        {
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = "5c90b312-aa30-4ca5-b36c-c75f3a348d3e"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "BusinessOwner",
                    NormalizedName = "BUSINESSOWNER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }, new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
        };

        private static IdentityUserRole<string>[] _userRole = new IdentityUserRole<string>[]
        {
                new IdentityUserRole<string>
                {
                    UserId = _users[0].Id,
                    RoleId = _roles[0].Id
                }
        };


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedRoles(modelBuilder, _roles);
            this.SeedUsers(modelBuilder, _users);
            this.SeedUserRoles(modelBuilder, _userRole);

            modelBuilder.Entity<UserProject>()
            .HasKey(x => new { x.UserId, x.ProjectId });

            modelBuilder.Entity<UserProject>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<UserProject>()
            .HasOne<Project>()
            .WithMany()
            .HasForeignKey(x => x.ProjectId);
            


        }

        private void SeedUsers(ModelBuilder modelBuilder, User[] users)
        {
            modelBuilder.Entity<User>().HasData(
                users
            );

        }

        private void SeedRoles(ModelBuilder modelBuilder, IdentityRole[] roles)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                roles
            );
        }

        private void SeedUserRoles(ModelBuilder modelBuilder, IdentityUserRole<string>[] UserRole)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                UserRole
            );
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }


    }
}


// new User
// {
// Id = Guid.NewGuid().ToString(),
// UserName = _configuration?["Admin:UserName"],
// NormalizedUserName = _configuration?["Admin:NormalizedUserName"],
// Email = _configuration?["Admin:Email"],
// NormalizedEmail = _configuration?["Admin:NormalizedEmail"],
// PasswordHash = _configuration?["Admin:PasswordHashed"],
// SecurityStamp = _configuration?["Admin:SecurityStamp"]
// }
