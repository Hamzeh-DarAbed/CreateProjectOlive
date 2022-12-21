using CreateProjectOlive.Repositories;
namespace CreateProjectOlive.UnitOfWorks
{

    public interface IUnitOfWork
    {

        IUserRepository User
        {
            get;
        }

        Task<int> SaveAsync();

        public void Dispose();
    }


}