using CreateProjectOlive.Models;
using MongoOlive.DBContext;

namespace CreateProjectOlive.Services
{
    public class ProjectService : Service<Project>, IProjectService
    {
        public ProjectService(ApplicationDBContext context) : base(context)
        {
        }
    }
}