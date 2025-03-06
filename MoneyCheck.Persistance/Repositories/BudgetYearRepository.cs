using Microsoft.EntityFrameworkCore;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.BudgetYears.Commands.CreateBudgetYear;
using MoneyCheck.Application.Features.Currencies;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class BudgetYearRepository(ApplicationDbContext dbContext, ISharedRepository sharedRepository) :
    BaseRepository<BudgetYear>(dbContext), IBudgetYearRepository
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<BudgetYearDto> AddBudgetYearAsync(CreateBudgetYear createBudgetYear)
    {
      var budgetYear = new BudgetYear
      {
        Id = 0,
        BudgetId = createBudgetYear.BudgetId,
        Year = createBudgetYear.Year,
      };

      using var transaction = _dbContext.Database.BeginTransaction();

      // Add budget year
      await _dbContext.BudgetYears.AddAsync(budgetYear);
      await _dbContext.SaveChangesAsync();

      // Get versions of current year
      var versions = await _sharedRepository.GetVersionsByBudgetId(budgetYear.BudgetId);

      // Add new version
      var yearVersions = await _sharedRepository.GetVersionsByYearId(budgetYear.Id);
      var version = new BudgetVersion
      {
        Id = 0,
        BudgetYearId = budgetYear.Id,
        VersionName = "v" + (yearVersions.Count() + 1).ToString(),
        DateCreated = DateTime.Today,
        IsClosed = false
      };

      await _dbContext.BudgetVersions.AddAsync(version);
      await _dbContext.SaveChangesAsync();

      if (createBudgetYear.Copy)
      {
        // Copy currencies and budget items from latest version
        var latestVersion = versions.MaxBy(x => x.Id);
        if (latestVersion != null)
        {
          // Copy currencies
          var currencies = await _sharedRepository.GetCurrenciesByVersionId(latestVersion.Id);
          createBudgetYear.Currencies = EntityMapper.CurrenciesToDto(currencies);
          AddCurrency(createBudgetYear.Currencies, version.Id);

          // Copy budget items
          var budgetItems = await _sharedRepository.GetBudgetItemsByVersionId(latestVersion.Id);
          AddBudgetItem(budgetItems, version.Id);
        }
      }
      else
      {
        // Add currencies
        AddCurrency(createBudgetYear.Currencies, version.Id);
      }

      await _dbContext.SaveChangesAsync();
      transaction.Commit(); // Commit transaction

      return EntityMapper.BudgetYearToDto(budgetYear);
    }

    public async Task DeleteBudgetYearAsync(int budgetYearId)
    {
      using var transaction = _dbContext.Database.BeginTransaction();

      var versions = await _dbContext.BudgetVersions
        .AsNoTracking()
        .Include(y => y.BudgetYear)
        .Where(y => y.BudgetYear != null && y.BudgetYear.Id == budgetYearId)
        .ToListAsync();

      foreach (var version in versions)
      {
        // Remove currencies
        var currencies = await _sharedRepository.GetCurrenciesByVersionId(version.Id, true);
        foreach (var currency in currencies)
          _dbContext.Currencies.Remove(currency);

        // Remove budget items
        var budgetItems = await _sharedRepository.GetBudgetItemsByVersionId(version.Id, true);
        foreach (var budgetItem in budgetItems)
          _dbContext.BudgetItems.Remove(budgetItem);

        // Remove version
        _dbContext.BudgetVersions.Remove(version);
      }
      await _dbContext.SaveChangesAsync();

      // Remove year
      var year = await GetByIdAsync(budgetYearId);
      if (year != null)
      {
        _dbContext.BudgetYears.Remove(year);
        await _dbContext.SaveChangesAsync();
      }

      transaction.Commit(); // Commit transaction
    }

    private async void AddCurrency(IEnumerable<CurrencyDto> currencies, int versionId)
    {
      foreach (var currency in currencies)
      {
        var currencyItem = new Currency
        {
          VersionId = versionId,
          Code = currency.Code,
          BudgetRate = currency.BudgetRate,
          AverageRate = currency.AverageRate
        };
        await _dbContext.Currencies.AddAsync(currencyItem);
      }
    }

    private async void AddBudgetItem(List<BudgetItem> budgetItems, int versionId)
    {
      foreach (var budgetItem in budgetItems)
      {
        var item = new BudgetItem
        {
          VersionId = versionId,
          CategoryId = budgetItem.CategoryId,
          UnitId = budgetItem.UnitId,
          CurrencyCode = budgetItem.CurrencyCode,
          UnitValue = budgetItem.UnitValue,
          Note = budgetItem.Note
        };
        await _dbContext.BudgetItems.AddAsync(item);
      }
    }
  }
}