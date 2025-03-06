using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface IPersonRepository : IAsyncRepository<Person>
  {
    Task<Person?> GetByEmail(string email);
  }
}