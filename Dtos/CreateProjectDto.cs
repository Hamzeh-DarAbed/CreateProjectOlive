using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreateProjectOlive.Dtos
{
    public class CreateProjectDto
    {
        [Required(ErrorMessage = "Project Name is required")]
        public string ProjectName { get; set; }=string.Empty;
        [Required(ErrorMessage = "Project Description is required")]
        public string ProjectDescription { get; set; }=string.Empty;
        [Required(ErrorMessage = "Business Type is required")]
        public string BusinessType { get; set; }=string.Empty;
        [Required(ErrorMessage = "Created By is required")]
        public string CreatedBy { get; set; }=string.Empty;
        [Required(ErrorMessage = "Domain is required")]
        public string Domain { get; set; }=string.Empty;
        
        
        
    }
}