using Microsoft.EntityFrameworkCore;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Features.BudgetVersions.Commands.CreateBudgetVersion;
using MoneyCheck.Application.Features.BudgetVersions.Commands.UpdateBudgetVersion;
using MoneyCheck.Application.Features.Currencies;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Application.Models.Localization;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class BudgetVersionRepository(ApplicationDbContext dbContext, ISharedRepository sharedRepository) :
    BaseRepository<BudgetVersion>(dbContext), IBudgetVersionRepository
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<ManageBudgetYear> GetCurrentVersion(int yearId)
    {
      var currentYear = await _sharedRepository.GetBudgetYearById(yearId);

      var budgetYearDTO = EntityMapper.BudgetYearToDto(currentYear);

      var budgetYears = await _sharedRepository.GetBudgetYearsByBudgetId(budgetYearDTO.BudgetId);
      var versions = await _sharedRepository.GetVersionsByYearId(yearId);

      var currentVersion = await _sharedRepository.GetCurrentVersionByYearId(yearId);
      var manageBudgetYear = new ManageBudgetYear
      {
        BudgetYear = EntityMapper.BudgetYearToDto(currentYear),
        BudgetYears = EntityMapper.BudgetYearsToDto(budgetYears),
        Versions = EntityMapper.BudgetVersionsToDto(versions)
      };

      if (currentVersion != null)
      {
        var currencies = await _sharedRepository.GetCurrenciesByVersionId(currentVersion.Id);

        manageBudgetYear.Version = EntityMapper.BudgetVersionToDto(currentVersion);
        manageBudgetYear.Currencies = EntityMapper.CurrenciesToDto(currencies);
      }

      return manageBudgetYear;
    }

    public async Task AddBudgetVersionAsync(CreateBudgetVersion createBudgetVersion)
    {
      using var transaction = _dbContext.Database.BeginTransaction();

      // Close current version
      var currentVersion = await _sharedRepository.GetCurrentVersionByYearId(createBudgetVersion.BudgetYearId);
      if (currentVersion != null)
        currentVersion.IsClosed = true;

      // Add new version
      var yearVersions = await _sharedRepository.GetVersionsByYearId(createBudgetVersion.BudgetYearId);
      var version = new BudgetVersion
      {
        Id = 0,
        BudgetYearId = createBudgetVersion.BudgetYearId,
        VersionName = "v" + (yearVersions.Count() + 1).ToString(),
        DateCreated = DateTime.Today,
        IsClosed = false
      };

      await _dbContext.BudgetVersions.AddAsync(version);
      await _dbContext.SaveChangesAsync(); // To get new version id

      // Add currencies
      AddCurrency(createBudgetVersion.Currencies, version.Id);

      if (createBudgetVersion.Copy && currentVersion != null)
      {
        // Copy Budget items
        var budgetItems = await _sharedRepository.GetBudgetItemsByVersionId(currentVersion.Id);
        AddBudgetItem(budgetItems, version.Id);
      }

      await _dbContext.SaveChangesAsync();
      transaction.Commit(); // Commit transaction
    }

    public async Task DeleteBudgetVersionAsync(int budgetYearId)
    {
      using var transaction = _dbContext.Database.BeginTransaction();

      var version = await _sharedRepository.GetCurrentVersionByYearId(budgetYearId);
      if (version != null)
      {
        // Reopen latest closed version
        var closedVersions = await GetClosedListByYearId(budgetYearId);
        var lastClosedVersion = closedVersions.OrderByDescending(i => i.Id).FirstOrDefault();
        if (lastClosedVersion != null)
          lastClosedVersion.IsClosed = false;

        // Remove currencies
        var currencies = await _sharedRepository.GetCurrenciesByVersionId(version.Id);
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
      transaction.Commit(); // Commit transaction
    }

    public async Task UpdateBudgetVersionAsync(UpdateBudgetVersion updateBudgetVersion)
    {
      var versionId = updateBudgetVersion.Version.Id;

      var versionItem = await GetById(versionId) ??
        throw new NotFoundException(LocaleErrorParam.BudgetVersion, "BudgetVersion", updateBudgetVersion.Version.Id);

      using var transaction = _dbContext.Database.BeginTransaction();

      versionItem.VersionName = updateBudgetVersion.Version.VersionName;
      //await _dbContext.SaveChangesAsync();

      var currencies = await _sharedRepository.GetCurrenciesByVersionId(versionId, true);
      foreach (var currency in currencies)
      {
        var currencyItem = updateBudgetVersion.Currencies.FirstOrDefault(c => c.Id == currency.Id);
        if (currencyItem != null)
        {
          currency.Code = currencyItem.Code;
          currency.BudgetRate = currencyItem.BudgetRate;
          currency.AverageRate = currencyItem.AverageRate;
        }
      }

      await _dbContext.SaveChangesAsync();
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

    private async Task<IEnumerable<BudgetVersion>> GetClosedListByYearId(int yearId)
    {
      return await _dbContext.BudgetVersions.AsNoTracking().Where(x => x.BudgetYearId == yearId && x.IsClosed).ToListAsync();
    }

    private async Task<BudgetVersion?> GetById(int versionId)
    {
      return await _dbContext.BudgetVersions.Where(x => x.Id == versionId).FirstOrDefaultAsync();
    }
  }
}