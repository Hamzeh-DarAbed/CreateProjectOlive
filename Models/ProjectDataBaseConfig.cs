using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateProjectOlive.Models
{
    public class ProjectDataBaseConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string Collection { get; set; } = string.Empty;

    }
}