using Core.Entities;
using Core.Helpers;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        List<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
        T Get(Expression<Func<T, bool>> filter);
		void Add(T entity);
        Task<int> AddWithCollection(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<int> UpdateWithCollections(T entity, bool withCollections = true);
        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate, bool eager = false, IEnumerable<string> includes = null);
        Task<IEnumerable<T>> Search(Expression<Func<T, T>> selector, Expression<Func<T, bool>> predicate = null);
        Task<int> UpdateFieldsSave(T entity, params Expression<Func<T, object>>[] includeProperties);
    }
}
