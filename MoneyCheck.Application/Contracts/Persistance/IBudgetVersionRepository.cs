using MoneyCheck.Application.Features.BudgetVersions.Commands.CreateBudgetVersion;
using MoneyCheck.Application.Features.BudgetVersions.Commands.UpdateBudgetVersion;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface IBudgetVersionRepository : IAsyncRepository<BudgetVersion>
  {
    Task<ManageBudgetYear> GetCurrentVersion(int yearId);

    Task AddBudgetVersionAsync(CreateBudgetVersion createBudgetVersion);

    Task UpdateBudgetVersionAsync(UpdateBudgetVersion updateBudgetVersion);

    Task DeleteBudgetVersionAsync(int budgetYearId);
  }
}