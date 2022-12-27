using CreateProjectOlive.Models;
using MongoOlive.DBContext;
using MongoOlive.Test.IntegrationTest;

namespace CreateProjectOlive.Services
{
    public class ProjectService : Service<Project>, IProjectService
    {
        public ProjectService(ApplicationDBContext context) : base(context)
        {
        }
    }
}