using System.ComponentModel.DataAnnotations;

namespace CreateProjectOlive.Dtos
{
    public class AddRoleDto
    {
        [Required]
        public string Name { get; set; } = null!;

    }
}