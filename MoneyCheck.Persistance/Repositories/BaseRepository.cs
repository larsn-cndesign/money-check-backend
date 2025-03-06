using Microsoft.EntityFrameworkCore;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class BaseRepository<T>(ApplicationDbContext dbContext) : IAsyncRepository<T> where T : class
  {
    protected readonly ApplicationDbContext _dbContext = dbContext;

    public virtual async Task<T?> GetByIdAsync(int id)
    {
      return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IList<T>> ListAllAsync()
    {
      return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
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
  }
}