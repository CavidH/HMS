using HMS.Core.Abstracts;
using HMS.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMS.Data.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        //private AppDbContext _context { get; }

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null, params string[] Includes)
        {
            //IQueryable<entity> query = _context.entitys.AsQueryable();
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

        public async Task<List<TEntity>> GetAllPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> expression = null, params string[] Includes)
        {
            var query = CheckQuery(Includes);
            return expression is null
                ? await query.Skip((page - 1) * size).Take(size).ToListAsync()
                : await query.Where(expression).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression is null
                ? await _context.Set<TEntity>().CountAsync()
                : await _context.Set<TEntity>().Where(expression).CountAsync();
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().AnyAsync(expression);
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

        //public async Task SaveAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

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
