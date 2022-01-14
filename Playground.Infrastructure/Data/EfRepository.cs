using Microsoft.EntityFrameworkCore;
using Playground.Core.Entities;
using Playground.Core.Interfaces;

namespace Playground.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly PlayDbContext _dbContext;

        public EfRepository(PlayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAsync(Func<IQueryable<T>, IQueryable<T>>? func = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            var resultQuery = func == null ? query : func(query);

            return await resultQuery.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync(Func<IQueryable<T>, IQueryable<T>>? func = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            var resultQuery = func == null ? query : func(query);

            return await resultQuery.CountAsync();
        }

        public async Task<T> FirstAsync(Func<IQueryable<T>, IQueryable<T>>? func = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            var resultQuery = func == null ? query : func(query);

            return await resultQuery.FirstAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Func<IQueryable<T>, IQueryable<T>>? func = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            var resultQuery = func == null ? query : func(query);

            return await resultQuery.FirstOrDefaultAsync();
        }

        public async Task<T> SingleAsync(Func<IQueryable<T>, IQueryable<T>>? func = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            var resultQuery = func == null ? query : func(query);

            return await resultQuery.SingleAsync();
        }

        public async Task<T?> SingleOrDefaultAsync(Func<IQueryable<T>, IQueryable<T>>? func = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            var resultQuery = func == null ? query : func(query);

            return await resultQuery.SingleOrDefaultAsync();
        }
    }
}
