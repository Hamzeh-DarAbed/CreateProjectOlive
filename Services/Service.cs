using MongoOlive.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CreateProjectOlive.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        protected readonly ApplicationDBContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Service(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);

                _context.SaveChanges();

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<TEntity> FindAll()
        {
            try
            {
                return _dbSet.AsQueryable();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return _dbSet.Where(expression).AsQueryable();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }



    }
}