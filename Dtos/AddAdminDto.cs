using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CreateProjectOlive.Dtos
{
    public class AddAdminDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

    }
}