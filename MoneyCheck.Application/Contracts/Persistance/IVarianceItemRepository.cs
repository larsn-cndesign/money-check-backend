using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface IVarianceItemRepository
  {
    Task<BudgetVariance> GetVarianceItems(ItemFilter filter);
  }
}