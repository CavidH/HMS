using System.Linq.Expressions;

namespace HMS.Core.Abstracts
{

    public interface IRepository<TEntity>
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null, params string[] Includes);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, params string[] Includes);

        Task<List<TEntity>> GetAllPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> expression = null,
            params string[] Includes);

        Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}