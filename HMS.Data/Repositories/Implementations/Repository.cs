using HMS.Core.Abstracts;
using HMS.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMS.Data.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null, params string[] Includes)
        {
            var query = CheckQuery(Includes);
            return expression is null
                ? await query.FirstOrDefaultAsync()
                : await query.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, params string[] Includes)
        {
            var query = CheckQuery(Includes);
            return expression is null
                ? await query.ToListAsync()
                : await query.Where(expression).ToListAsync();
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

      

        private IQueryable<TEntity> CheckQuery(params string[] Includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}
