using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreateProjectOlive.Dtos
{
    public class CreateProjectDto
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectDescription { get; set; }
        [Required]
        public string BusinessType { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string Domain { get; set; }
        
        
    }
}