using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Services;

namespace CreateProjectOlive.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        public IProjectService ProjectService { get; }

        public UnitOfWork(IProjectService projectService)
        {
            ProjectService = projectService;
        }
    }
    
}