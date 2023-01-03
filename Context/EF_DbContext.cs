using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CreateProjectOlive.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CreateProjectOlive.Context
{
    public class EF_DbContext : IdentityDbContext<User>
    {
        private readonly SeedIdentityUserOptions _seedIdentityUserOptions;
        private readonly SeedRoleOptions _seedRoleOptions;



        private User[] _users;
        private IdentityRole[] _roles;

        private IdentityUserRole<string>[] _userRole;
        public EF_DbContext(DbContextOptions<EF_DbContext> options, IOptions<SeedIdentityUserOptions> seedIdentityUserOptions, IOptions<SeedRoleOptions> seedRoleOptions) : base(options)
        {
            _seedIdentityUserOptions = seedIdentityUserOptions.Value;
            _seedRoleOptions = seedRoleOptions.Value;


            _users = new User[]
                {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = _seedIdentityUserOptions!.UserName,
                    NormalizedUserName = _seedIdentityUserOptions.NormalizedUserName,
                    Email = _seedIdentityUserOptions.Email,
                    NormalizedEmail = _seedIdentityUserOptions.NormalizedEmail,
                    PasswordHash = _seedIdentityUserOptions.PasswordHash,
                    SecurityStamp = _seedIdentityUserOptions.SecurityStamp
    }
    };

            _roles = new IdentityRole[]
                    {
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _seedRoleOptions.roles![0].Name,
                    NormalizedName = _seedRoleOptions.roles[0].NormalizedName,
                    ConcurrencyStamp = _seedRoleOptions.roles[0].ConcurrencyStamp
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _seedRoleOptions.roles![1].Name,
                    NormalizedName = _seedRoleOptions.roles[1].NormalizedName,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }, new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _seedRoleOptions.roles![2].Name,
                    NormalizedName = _seedRoleOptions.roles[2].NormalizedName,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
                    };

            _userRole = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>
                {
                    UserId = _users[0].Id,
                    RoleId = _roles[0].Id
                }
            };


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedRoles(modelBuilder, _roles);
            this.SeedUsers(modelBuilder, _users);
            this.SeedUserRoles(modelBuilder, _userRole);
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
    }
}