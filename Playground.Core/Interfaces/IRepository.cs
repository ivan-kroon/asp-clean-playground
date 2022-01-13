using Playground.Core.Entities;

namespace Playground.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity         
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAsync(Func<IQueryable<T>, IQueryable<T>>? func = null);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(Func<IQueryable<T>, IQueryable<T>>? func = null);
        Task<T> FirstAsync(Func<IQueryable<T>, IQueryable<T>>? func = null);
        Task<T?> FirstOrDefaultAsync(Func<IQueryable<T>, IQueryable<T>>? func = null);
        Task<T> SingleAsync(Func<IQueryable<T>, IQueryable<T>>? func = null);
        Task<T?> SingleOrDefaultAsync(Func<IQueryable<T>, IQueryable<T>>? func = null);
    }
}
