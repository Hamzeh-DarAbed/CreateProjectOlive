using CreateProjectOlive.Repositories;
using CreateProjectOlive.Services;

namespace CreateProjectOlive.UnitOfWorks
{

    public interface IUnitOfWork : IDisposable
    {

        IProjectService ProjectService { get; }
        IUserRepository User
        {
            get;
        }

        Task<int> SaveAsync();


        public void Dispose();
    }


}