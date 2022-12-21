using Microsoft.EntityFrameworkCore;
using CreateProjectOlive.Context;
using CreateProjectOlive.Models;
using CreateProjectOlive.UnitOfWorks;


namespace CreateProjectOlive.Test
{
    public class OliveTestBase : IDisposable
    {
        protected EF_DbContext _context;
        protected IUnitOfWork _unitOfWork;

        public OliveTestBase()
        {
            var options = new DbContextOptionsBuilder<EF_DbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new EF_DbContext(options);
            _context.Database.EnsureCreated();
            Seed(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

        public virtual void Seed(EF_DbContext _context)
        {
            var user = new[]{
                new User
                {
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@optimumpartners.com",
                    NormalizedEmail = "ADMIN@OPTIMUMPARTNERS.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI99MKFTBxu+rmOvvm8PZbXbVHCLtDa57L+/ZS/68kKU6e0OcP5Sg+U6+TYJ3s1yFg==",
                    SecurityStamp = "P4HECIURGID4GGDNIUF24PQTFTTYDCER"
                }
        };

            _context.Users!.AddRange(user);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}