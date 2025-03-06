using MoneyCheck.Application.Features.BudgetItems;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface IBudgetItemRepository : IAsyncRepository<BudgetItem>
  {
    Task<ManageBudgetItem> GetBudgetItems(ItemFilter filter);

    Task<BudgetItemDto> AddBudgetItemAsync(BudgetItemDto budgetItem);

    Task UpdateBudgetItem(BudgetItemDto budgetItem);
  }
}