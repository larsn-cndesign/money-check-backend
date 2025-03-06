using Microsoft.EntityFrameworkCore;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Features.ActualItems;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Application.Models.Localization;
using MoneyCheck.Domain.Enteties;
using MoneyCheck.Persistance.Contexts;

namespace MoneyCheck.Persistance.Repositories
{
  public class ActualItemRepository(ApplicationDbContext dbContext, ISharedRepository sharedRepository) :
    BaseRepository<ActualItem>(dbContext), IActualItemRepository
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<ManageActualItem> GetActualItems(ItemFilter filter)
    {
      var budgetYears = await _sharedRepository.GetBudgetYearsByBudgetId(filter.BudgetId);
      if (!budgetYears.Any())
        return new ManageActualItem();

      var years = filter.BudgetYearId == -1 ? budgetYears : budgetYears.Where(b => b.Id == filter.BudgetYearId).ToList();

      var currencyItems = new List<CurrencyItem>();
      var actualItems = new List<ActualItem>();

      // Get open currencies and transactions for selected years
      foreach (var budgetYear in years)
      {
        var currentVersion = await _sharedRepository.GetCurrentVersionByYearId(budgetYear.Id);

        // Get currencies per year
        if (currentVersion != null)
        {
          var currencyList = await _sharedRepository.GetCurrenciesByVersionId(currentVersion.Id);
          currencyItems.AddRange(currencyList.Select(c => new CurrencyItem
          {
            CurrencyCode = c.Code,
            BudgetRate = c.BudgetRate,
            AverageRate = c.AverageRate
          }));
        }

        // Transactions
        var transactions = await _dbContext.ActualItems
          .AsNoTracking()
          .Where(a => a.BudgetId == filter.BudgetId && a.PurchaseDate.Year == budgetYear.Year)
          .ToListAsync();
        actualItems.AddRange(transactions);
      }

      // Get uniqe currencies for selected period
      var currencies = currencyItems.Select(c => c.CurrencyCode).Distinct().ToList();

      // Apply Filters
      actualItems = actualItems
          .Where(a => string.IsNullOrWhiteSpace(filter.CurrencyCode) || a.CurrencyCode == filter.CurrencyCode)
          .Where(a => filter.CategoryId == -1 || a.CategoryId == filter.CategoryId)
          .Where(a => filter.TripId == -1 || a.TripId == filter.TripId)
          .Where(a => string.IsNullOrWhiteSpace(filter.Note) || (a.Note ?? "").Contains(filter.Note, StringComparison.CurrentCultureIgnoreCase))
          .ToList();

      var item = new ManageActualItem
      {
        Filter = filter,
        BudgetYears = EntityMapper.BudgetYearsToDto(budgetYears),
        Currencies = currencies,
        Categories = EntityMapper.CategoriesToDto(await _sharedRepository.GetCategoriesByBudgetId(filter.BudgetId)),
        Trips = EntityMapper.TripsToDto(await _sharedRepository.GetTripsByBudgetId(filter.BudgetId, false)),
        CurrencyItems = currencyItems,

        ActualItems = EntityMapper.ActualItemsToDto(
        [.. actualItems
          .Select(
            t =>
              new ActualItem
              {
                Id = t.Id,
                BudgetId = t.BudgetId,
                CategoryId = t.CategoryId,
                TripId = t.TripId != null ? t.TripId : -1,
                PurchaseDate = t.PurchaseDate,
                CurrencyCode = t.CurrencyCode,
                Amount = t.Amount,
                Note = t.Note
              }
          )
          .OrderByDescending(o => o.PurchaseDate)])
      };

      // Enrich Actual Items with Category & Trip Info
      var categoryLookup = item.Categories.ToDictionary(c => c.Id);
      var tripLookup = item.Trips.ToDictionary(t => t.Id);

      foreach (var actualItem in item.ActualItems)
      {
        if (categoryLookup.TryGetValue(actualItem.CategoryId, out var category))
          actualItem.CategoryName = category?.CategoryName ?? "";

        if (actualItem.TripId != null && tripLookup.TryGetValue(actualItem.TripId.Value, out var trip))
          actualItem.TripName = $"{trip.FromDate:yyyy-MM-dd} / {trip.ToDate:yyyy-MM-dd}";
      }

      return item;
    }

    public async Task<ActualItemDto> AddActualItemAsync(ActualItemDto actualItem)
    {
      var item = new ActualItem
      {
        BudgetId = actualItem.BudgetId,
        CategoryId = actualItem.CategoryId,
        TripId = actualItem.TripId != -1 ? actualItem.TripId : null,
        PurchaseDate = actualItem.PurchaseDate,
        CurrencyCode = actualItem.CurrencyCode,
        Amount = actualItem.Amount,
        Note = string.IsNullOrWhiteSpace(actualItem.Note) ? null : actualItem.Note
      };

      _dbContext.ActualItems.Add(item);
      await _dbContext.SaveChangesAsync();

      actualItem.Id = item.Id;

      return actualItem;
    }

    public async Task UpdateActualItem(ActualItemDto actualItem)
    {
      var item = await _dbContext.ActualItems.FindAsync(actualItem.Id) ??
        throw new NotFoundException(LocaleErrorParam.ActualItem, "ActualItem", actualItem.Id);

      item.CategoryId = actualItem.CategoryId;
      item.TripId = actualItem.TripId != -1 ? actualItem.TripId : null;
      item.CurrencyCode = actualItem.CurrencyCode;
      item.PurchaseDate = actualItem.PurchaseDate;
      item.Amount = actualItem.Amount;
      item.Note = string.IsNullOrWhiteSpace(actualItem.Note) ? null : actualItem.Note;

      await _dbContext.SaveChangesAsync();
    }
  }
}