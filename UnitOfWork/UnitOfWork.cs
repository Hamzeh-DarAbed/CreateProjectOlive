using CreateProjectOlive.Context;
using CreateProjectOlive.Repositories;
using CreateProjectOlive.Services;

namespace CreateProjectOlive.UnitOfWorks
{

    public class UnitOfWork : IUnitOfWork
    {
        private EF_DbContext _context;

        public UnitOfWork(EF_DbContext EF_DBContext)
        {
            this._context = EF_DBContext;
            this.User = new UserRepository(this._context);
            this.ProjectService = new ProjectService(this._context);
        }

        public IUserRepository User
        {
            get;
        }

        public IProjectService ProjectService
        {
            get;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            _context.Dispose();
            return await _context.SaveChangesAsync();
        }
    }

}