using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateProjectOlive.Dtos
{
    public class LoginUserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}