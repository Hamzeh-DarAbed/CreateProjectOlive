using CreateProjectOlive.Context;

namespace CreateProjectOlive.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EF_DbContext _context;

        public GenericRepository(EF_DbContext context)
        {
            this._context = context;
        }
        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
    }
}