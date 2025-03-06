using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class BudgetRepository(ApplicationDbContext dbContext) : BaseRepository<Budget>(dbContext), IBudgetRepository
  {
  }
}