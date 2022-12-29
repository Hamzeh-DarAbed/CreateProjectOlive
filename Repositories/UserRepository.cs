using CreateProjectOlive.Models;
using CreateProjectOlive.Context;

namespace CreateProjectOlive.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EF_DbContext context) : base(context) { }

        public async Task<bool> IsExistsEmail(string email)
        {
            var user = await _context.Users.FindAsync(email);
            if (user != null)
            {
                return true;
            }
            return false;
        }


    }
}