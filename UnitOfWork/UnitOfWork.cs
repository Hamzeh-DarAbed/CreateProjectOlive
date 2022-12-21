using CreateProjectOlive.Context;
using CreateProjectOlive.Repositories;

namespace CreateProjectOlive.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private EF_DbContext _context;

        public UnitOfWork(EF_DbContext EF_DBContext)
        {
            this._context = EF_DBContext;
            this.User = new UserRepository(this._context);
        }

        public IUserRepository User
        {
            get;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}