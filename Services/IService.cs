using System.Linq.Expressions;
using CreateProjectOlive.Models;

namespace CreateProjectOlive.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity entity,User? user);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

}