using System.ComponentModel.DataAnnotations;

namespace CreateProjectOlive.Dtos
{
    public class AddAdminDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

    }
}