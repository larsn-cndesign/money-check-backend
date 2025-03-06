namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface IAsyncRepository<T> where T : class
  {
    Task<T?> GetByIdAsync(int id);

    Task<IList<T>> ListAllAsync();

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
  }
}