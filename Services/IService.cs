using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CreateProjectOlive.Services
{
    public interface IService<TDocument> where TDocument : class
    {
        Task<IEnumerable<TDocument>> GetAll();
        Task<TDocument> GetById(string id);
        Task Create(TDocument project);

        Task<bool> Update(string id, TDocument project);
        Task<bool> Delete(string id);
    }

}