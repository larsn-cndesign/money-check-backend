using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface ISharedRepository
  {
    Task<BudgetYear> GetBudgetYearById(int yearId);

    Task<IEnumerable<BudgetYear>> GetBudgetYearsByBudgetId(int budgetId);

    Task<IEnumerable<BudgetVersion>> GetVersionsByYearId(int yearId);

    Task<IEnumerable<BudgetVersion>> GetVersionsByBudgetId(int budgetId);

    Task<BudgetVersion> GetCurrentVersionByYearId(int yearId);

    Task<IEnumerable<Currency>> GetCurrenciesByVersionId(int versionId, bool track = false);

    Task<IEnumerable<Category>> GetCategoriesByBudgetId(int budgetId);

    Task<IEnumerable<Unit>> GetUnitsByBudgetId(int budgetId);

    Task<List<BudgetItem>> GetBudgetItemsByVersionId(int versionId, bool track = false);

    Task<IEnumerable<Trip>> GetTripsByBudgetId(int budgetId, bool asc = true);

    Task<IEnumerable<BudgetItem>> GetBudgetItemsByCategoryAndVersion(int categoryId, int versionId);

    Task<IEnumerable<ActualItem>> GetActualItemsByCategoryYearMonth(int categoryId, int Year, int month);
  }
}