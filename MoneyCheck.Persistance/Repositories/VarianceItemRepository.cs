using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Application.Models.Localization;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Repositories
{
  public class VarianceItemRepository(ISharedRepository sharedRepository) : IVarianceItemRepository
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<BudgetVariance> GetVarianceItems(ItemFilter filter)
    {
      var budgetYears = await _sharedRepository.GetBudgetYearsByBudgetId(filter.BudgetId);
      if (!budgetYears.Any())
        return new BudgetVariance();

      // Year
      filter.BudgetYearId = filter.BudgetYearId == -1 ? budgetYears.Max(b => b.Id) : filter.BudgetYearId;
      var budgetYear = await _sharedRepository.GetBudgetYearById(filter.BudgetYearId) ??
        throw new BadRequestException(new LocaleError(LocaleErrorKey.NotFound, LocaleErrorParam.BudgetYear).ToJson());

      // Version
      var versions = await _sharedRepository.GetVersionsByYearId(filter.BudgetYearId);
      var version = (filter.VersionId == -1
          ? await _sharedRepository.GetCurrentVersionByYearId(budgetYear.Id)
          : versions.FirstOrDefault(v => v.Id == filter.VersionId)) ??
          throw new BadRequestException(new LocaleError(LocaleErrorKey.NotFound, LocaleErrorParam.BudgetVersion).ToJson());

      filter.VersionId = version.Id;

      // Currency
      var currencies = await _sharedRepository.GetCurrenciesByVersionId(version.Id);
      var currency = currencies.FirstOrDefault(x => x.Code == filter.CurrencyCode) ?? currencies.FirstOrDefault() ??
        throw new BadRequestException(new LocaleError(LocaleErrorKey.NotFound, LocaleErrorParam.Currency).ToJson());
      filter.CurrencyCode = currency.Code;

      // Category
      var categories = await _sharedRepository.GetCategoriesByBudgetId(filter.BudgetId);
      var filteredCategories = filter.List.Any(x => x.Selected)
          ? categories.Where(c => filter.List.Any(x => x.Selected && x.Name == c.CategoryName)).ToList()
          : categories;

      // Get number of days traveled
      var travelDayCount = filter.TravelDay ? await GetTotalDaysForMonthOrYear(filter.BudgetId, budgetYear.Year, filter.Month) : 1;
      filter.TravelDayCount = travelDayCount;
      var travelDayDivider = travelDayCount != 0 ? travelDayCount : 1;

      var varianceItems = new List<VarianceItem>();

      // Sum category
      foreach (var category in filteredCategories)
      {
        var actual = await SumActuals(category.Id, budgetYear.Year, filter.Month, currency, currencies) / travelDayDivider;
        var budget = (await SumBudgets(category.Id, version.Id, currency, currencies) / travelDayDivider) / (filter.Month != -1 ? 12 : 1);

        varianceItems.Add(new VarianceItem
        {
          Category = category.CategoryName ?? "",
          Actual = actual,
          Budget = budget,
          Variance = budget - actual
        });
      }

      // Calculate totals
      var totalActual = varianceItems.Sum(a => a.Actual);
      var totalBudget = varianceItems.Sum(b => b.Budget);

      return new BudgetVariance
      {
        BudgetYears = EntityMapper.BudgetYearsToDto(budgetYears),
        Version = EntityMapper.BudgetVersionToDto(version),
        Versions = EntityMapper.BudgetVersionsToDto(versions),
        Currency = EntityMapper.CurrencyToDto(currency),
        Currencies = EntityMapper.CurrenciesToDto(currencies),
        Categories = EntityMapper.CategoriesToDto(categories),
        VarianceItem = new VarianceItem
        {
          Actual = totalActual,
          Budget = totalBudget,
          Variance = totalBudget - totalActual
        },
        VarianceItems = varianceItems,
        Filter = filter
      };
    }

    private static decimal SumCurrency(decimal val, string code, Currency currency, Dictionary<string, Currency> currencyDict, bool isActual)
    {
      if (string.IsNullOrEmpty(code) || code == currency.Code)
        return val;

      if (currencyDict.TryGetValue(code, out var convertCurrency))
      {
        return isActual
            ? val * (convertCurrency.AverageRate / currency.AverageRate)
            : val * (convertCurrency.BudgetRate / currency.BudgetRate);
      }

      return val; // Default case if currency conversion not found
    }

    private async Task<decimal> SumActuals(int categoryId, int year, int month, Currency currency, IEnumerable<Currency> currencies)
    {
      var actuals = await _sharedRepository.GetActualItemsByCategoryYearMonth(categoryId, year, month);

      var currencyDict = currencies.ToDictionary(c => c.Code, c => c);

      return actuals.Sum(val => SumCurrency(val.Amount, val.CurrencyCode, currency, currencyDict, true));
    }

    private async Task<decimal> SumBudgets(int categoryId, int versionId, Currency currency, IEnumerable<Currency> currencies)
    {
      var budgets = await _sharedRepository.GetBudgetItemsByCategoryAndVersion(categoryId, versionId);

      var currencyDict = currencies.ToDictionary(c => c.Code, c => c);

      var sumBudgets = budgets.Aggregate(
          1.0m,
          (acc, val) => acc * SumCurrency(val.UnitValue, val.CurrencyCode!, currency, currencyDict, false)
      );

      return sumBudgets == 1.0m ? 0.0m : sumBudgets;
    }

    private async Task<int> GetTotalDaysForMonthOrYear(int budgetId, int year, int month)
    {
      var trips = await _sharedRepository.GetTripsByBudgetId(budgetId);

      int totalDays = 0;

      DateTime periodStart, periodEnd;

      if (month == -1)
      {
        // Whole year
        periodStart = new DateTime(year, 1, 1);
        periodEnd = new DateTime(year, 12, 31);
      }
      else
      {
        // Specific month
        periodStart = new DateTime(year, month, 1);
        periodEnd = periodStart.AddMonths(1).AddDays(-1);
      }

      foreach (var trip in trips)
      {
        // Get the intersection of the trip with the period
        DateTime tripStart = trip.FromDate > periodStart ? trip.FromDate : periodStart;
        DateTime tripEnd = trip.ToDate < periodEnd ? trip.ToDate : periodEnd;

        if (tripStart <= tripEnd) // Ensure the trip is valid
        {
          totalDays += (tripEnd - tripStart).Days + 1; // Include the last day
        }
      }

      return totalDays;
    }
  }
}