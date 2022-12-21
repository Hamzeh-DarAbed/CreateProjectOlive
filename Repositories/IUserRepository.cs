using CreateProjectOlive.Models;

namespace CreateProjectOlive.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsExistsEmail(string email);


    }
}