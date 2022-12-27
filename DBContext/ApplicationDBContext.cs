using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Models;
using Microsoft.EntityFrameworkCore;

namespace MongoOlive.DBContext
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }




    }
}