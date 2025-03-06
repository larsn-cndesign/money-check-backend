using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.BudgetYears.Commands.CreateBudgetYear;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface IBudgetYearRepository : IAsyncRepository<BudgetYear>
  {
    Task<BudgetYearDto> AddBudgetYearAsync(CreateBudgetYear createBudgetYear);

    Task DeleteBudgetYearAsync(int budgetYearId);
  }
}