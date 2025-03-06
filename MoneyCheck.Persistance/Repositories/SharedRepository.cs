using Microsoft.EntityFrameworkCore;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Localization;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class SharedRepository(ApplicationDbContext dbContext) : ISharedRepository
  {
    // Budget year
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<BudgetYear> GetBudgetYearById(int yearId)
    {
      return await _dbContext.BudgetYears.FindAsync(yearId) ??
        throw new NotFoundException(LocaleErrorParam.BudgetYear, "BudgetYear", yearId);
    }

    public async Task<IEnumerable<BudgetYear>> GetBudgetYearsByBudgetId(int budgetId)
    {
      var budgetYearsQuery = _dbContext.BudgetYears
        .AsNoTracking()
        .Where(x => x.BudgetId == budgetId)
        .OrderBy(x => x.Year);

      return await budgetYearsQuery.ToListAsync();
    }

    // Version
    public async Task<IEnumerable<BudgetVersion>> GetVersionsByBudgetId(int budgetId)
    {
      return await _dbContext.BudgetVersions
        .AsNoTracking()
        .Include(y => y.BudgetYear)
        .Where(b => b.BudgetYear != null && b.BudgetYear.BudgetId == budgetId).ToListAsync();
    }

    public async Task<IEnumerable<BudgetVersion>> GetVersionsByYearId(int yearId)
    {
      return await _dbContext.BudgetVersions.AsNoTracking().Where(x => x.BudgetYearId == yearId).ToListAsync();
    }

    public async Task<BudgetVersion> GetCurrentVersionByYearId(int yearId)
    {
      return await _dbContext.BudgetVersions
        .AsNoTracking()
        .Where(x => x.BudgetYearId == yearId && !x.IsClosed)
        .FirstOrDefaultAsync() ??
        throw new NotFoundException(LocaleErrorParam.BudgetVersion, "BudgetVersion", yearId);
    }

    // Budget item
    public async Task<List<BudgetItem>> GetBudgetItemsByVersionId(int versionId, bool track = false)
    {
      var result = track ?
        _dbContext.BudgetItems.Where(b => b.VersionId == versionId).ToListAsync() :
        _dbContext.BudgetItems.AsNoTracking().Where(b => b.VersionId == versionId).ToListAsync();

      return await result;
    }

    public async Task<IEnumerable<BudgetItem>> GetBudgetItemsByCategoryAndVersion(int categoryId, int versionId)
    {
      return await _dbContext.BudgetItems.Where(b => b.CategoryId == categoryId && b.VersionId == versionId).ToListAsync();
    }

    // Actual item
    public async Task<IEnumerable<ActualItem>> GetActualItemsByCategoryYearMonth(int categoryId, int Year, int month)
    {
      if (month != -1)
      {
        return await _dbContext.ActualItems
          .Where(a => a.CategoryId == categoryId && a.PurchaseDate.Year == Year && a.PurchaseDate.Month == month)
          .ToListAsync();
      }
      else
      {
        return await _dbContext.ActualItems
          .Where(a => a.CategoryId == categoryId && a.PurchaseDate.Year == Year)
          .ToListAsync();
      }
    }

    // Currency
    public async Task<IEnumerable<Currency>> GetCurrenciesByVersionId(int versionId, bool track = false)
    {
      var result = track ?
        _dbContext.Currencies.Where(x => x.VersionId == versionId) :
        _dbContext.Currencies.AsNoTracking().Where(x => x.VersionId == versionId);

      return await result.OrderBy(c => c.Code).ToListAsync();
    }

    // Category
    public async Task<IEnumerable<Category>> GetCategoriesByBudgetId(int budgetId)
    {
      var categoriesQuery = _dbContext.Categories.AsNoTracking().Where(x => x.BudgetId == budgetId)
        .OrderBy(c => c.CategoryName);

      return await categoriesQuery.ToListAsync();
    }

    // Unit
    public async Task<IEnumerable<Unit>> GetUnitsByBudgetId(int budgetId)
    {
      var unitsQuery = _dbContext.Units.AsNoTracking().Where(x => x.BudgetId == budgetId)
        .OrderBy(c => c.UnitName);

      return await unitsQuery.ToListAsync();
    }

    // Trip
    public async Task<IEnumerable<Trip>> GetTripsByBudgetId(int budgetId, bool asc = true)
    {
      var tripsQuery = _dbContext.Trips.AsNoTracking().Where(x => x.BudgetId == budgetId);

      // Apply sorting before fetching the data
      tripsQuery = asc
          ? tripsQuery.OrderBy(c => c.FromDate)
          : tripsQuery.OrderByDescending(c => c.FromDate);

      return await tripsQuery.ToListAsync();
    }
  }
}