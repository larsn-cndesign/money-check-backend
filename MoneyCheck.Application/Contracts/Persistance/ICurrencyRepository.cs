using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface ICurrencyRepository : IAsyncRepository<Currency>
  {
  }
}