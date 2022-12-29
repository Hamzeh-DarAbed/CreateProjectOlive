using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Models;

namespace MongoOlive.Models
{
    public class UserProject
    {
        public Guid ProjectId { get; set; }
        public string UserId { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
        
    }
}