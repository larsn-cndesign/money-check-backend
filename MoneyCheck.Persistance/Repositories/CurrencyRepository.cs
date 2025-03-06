using Microsoft.EntityFrameworkCore;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class CurrencyRepository(ApplicationDbContext dbContext) : BaseRepository<Currency>(dbContext), ICurrencyRepository
  {
  }
}