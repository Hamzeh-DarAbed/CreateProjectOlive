using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CreateProjectOlive.Services
{
    public class ProjectService : Service<Project>, IProjectService
    {
        public ProjectService(IOptions<ProjectDataBaseConfig> options) : base(options)
        {
        }
    }
}