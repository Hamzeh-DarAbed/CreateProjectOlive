using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateProjectOlive.Dtos
{
    public class UpdateProjectDto
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string BusinessType { get; set; }
        public string CreatedBy { get; set; }
        public string Domain { get; set; }
    }
}