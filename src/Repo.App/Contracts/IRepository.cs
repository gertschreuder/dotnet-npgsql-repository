using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repo.App.Contracts
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}