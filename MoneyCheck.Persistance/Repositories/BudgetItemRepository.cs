using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Features.BudgetItems;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Application.Models.Localization;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class BudgetItemRepository(ApplicationDbContext dbContext, ISharedRepository sharedRepository) :
    BaseRepository<BudgetItem>(dbContext), IBudgetItemRepository
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<ManageBudgetItem> GetBudgetItems(ItemFilter filter)
    {
      var budgetYears = await _sharedRepository.GetBudgetYearsByBudgetId(filter.BudgetId);
      if (!budgetYears.Any())
        return new ManageBudgetItem();

      // Determine BudgetYearId (latest or selected)
      filter.BudgetYearId = filter.BudgetYearId == -1
          ? budgetYears.Max(i => i.Id)
          : budgetYears.FirstOrDefault(y => y.Id == filter.BudgetYearId)?.Id ?? -1;

      // Fetch data
      var categories = await _sharedRepository.GetCategoriesByBudgetId(filter.BudgetId);
      var units = await _sharedRepository.GetUnitsByBudgetId(filter.BudgetId);
      var currentVersion = await _sharedRepository.GetCurrentVersionByYearId(filter.BudgetYearId);

      if (currentVersion == null)
        return new ManageBudgetItem();

      // Fetch items and currencies
      var currencies = await _sharedRepository.GetCurrenciesByVersionId(currentVersion.Id);
      var budgetItemList = await _sharedRepository.GetBudgetItemsByVersionId(currentVersion.Id);

      var budgetItems = EntityMapper.BudgetItemsToDto(budgetItemList);

      // Apply category filter if needed
      if (filter.CategoryId != -1)
        budgetItems = budgetItems.Where(b => b.CategoryId == filter.CategoryId).ToList();

      // Convert categories and units to dictionary for O(1) lookup instead of O(n)
      var categoryDict = categories.ToDictionary(c => c.Id, c => c.CategoryName ?? "");
      var unitDict = units.ToDictionary(u => u.Id, u => u.UnitName ?? "");

      // Assign category and unit names
      foreach (var budgetItem in budgetItems)
      {
        budgetItem.CategoryName = categoryDict.GetValueOrDefault(budgetItem.CategoryId, "");
        budgetItem.UnitName = unitDict.GetValueOrDefault(budgetItem.UnitId, "");
      }

      return new ManageBudgetItem
      {
        Filter = filter,
        BudgetYears = EntityMapper.BudgetYearsToDto(budgetYears),
        Categories = EntityMapper.CategoriesToDto(categories),
        Units = EntityMapper.UnitsToDto(units),
        Version = EntityMapper.BudgetVersionToDto(currentVersion),
        Currencies = EntityMapper.CurrenciesToDto(currencies),
        BudgetItems = [.. budgetItems.OrderBy(c => c.CategoryName, Comparers.Swedish)]
      };
    }

    public async Task<BudgetItemDto> AddBudgetItemAsync(BudgetItemDto budgetItem)
    {
      var item = new BudgetItem
      {
        VersionId = budgetItem.VersionId,
        CategoryId = budgetItem.CategoryId,
        UnitId = budgetItem.UnitId,
        CurrencyCode = budgetItem.CurrencyCode,
        UnitValue = budgetItem.UnitValue,
        Note = string.IsNullOrWhiteSpace(budgetItem.Note) ? null : budgetItem.Note
      };

      _dbContext.BudgetItems.Add(item);
      await _dbContext.SaveChangesAsync();

      budgetItem.Id = item.Id;

      return budgetItem;
    }

    public async Task UpdateBudgetItem(BudgetItemDto budgetItem)
    {
      var item = await _dbContext.BudgetItems.FindAsync(budgetItem.Id) ??
        throw new NotFoundException(LocaleErrorParam.BudgetItem, "BudgetItem", budgetItem.Id);

      item.CategoryId = budgetItem.CategoryId;
      item.UnitId = budgetItem.UnitId;
      item.CurrencyCode = budgetItem.CurrencyCode;
      item.UnitValue = budgetItem.UnitValue;
      item.Note = string.IsNullOrWhiteSpace(budgetItem.Note) ? null : budgetItem.Note;

      await _dbContext.SaveChangesAsync();
    }
  }
}