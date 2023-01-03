using CreateProjectOlive.ModelInterfaces;
using Microsoft.AspNetCore.Identity;

namespace CreateProjectOlive.Models
{
    public class User : IdentityUser, IUser
    {

    }
}