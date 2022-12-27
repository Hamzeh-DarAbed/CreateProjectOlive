using CreateProjectOlive.Services;
using MongoOlive.DBContext;

namespace CreateProjectOlive.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _config;
        public IProjectService ProjectService { get; }


        public UnitOfWork(ApplicationDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            ProjectService = new ProjectService(_context);
        }

        
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}