using CreateProjectOlive.Context;
using CreateProjectOlive.Models;
using MongoOlive.Models;

namespace CreateProjectOlive.Services
{
    public class ProjectService : Service<Project>, IProjectService
    {
        public ProjectService(EF_DbContext EF_DBContext) : base(EF_DBContext)
        {
        }

        override public void Create(Project entity, User? user)
        {
            try
            {
                _dbSet.Add(entity);
                UserProject userProject = new UserProject();
                userProject.Project = entity;
                userProject.User = user;

                userProject.UserId = user.Id;

                userProject.ProjectId = entity.Id;
                _context.UserProjects.Add(userProject);

                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}