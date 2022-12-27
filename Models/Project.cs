using System.ComponentModel.DataAnnotations;

namespace CreateProjectOlive.Models
{
    public record Project
    {
        
        public string Id { get; set; } 
        [Required(ErrorMessage = "Project Name is required")]
        public string ProjectName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Project Description is required")]
        public string ProjectDescription { get; set; } = string.Empty;
        [Required(ErrorMessage = "Business Type is required")]
        public string BusinessType { get; set; } = string.Empty;
        [Required(ErrorMessage = "Created By is required")]
        public string CreatedBy { get; set; } = string.Empty;
        [Required(ErrorMessage = "Domain is required")]
        public string Domain { get; set; } = string.Empty;
        


    }
}