using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Services;

namespace CreateProjectOlive.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProjectService ProjectService { get; }
    }
}