using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateProjectOlive.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task Create(T entity);

    }
}