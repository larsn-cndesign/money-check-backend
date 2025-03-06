using MoneyCheck.Application.Features.ActualItems;
using MoneyCheck.Application.Features.BudgetItems;
using MoneyCheck.Application.Features.Budgets;
using MoneyCheck.Application.Features.BudgetVersions;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.Categories;
using MoneyCheck.Application.Features.Currencies;
using MoneyCheck.Application.Features.Person;
using MoneyCheck.Application.Features.Trips;
using MoneyCheck.Application.Features.Units;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Mapping
{
  public class EntityMapper
  {
    // Trip
    public static TripDto TripToDto(Trip trip) =>
      new()
      {
        Id = trip.Id,
        FromDate = trip.FromDate,
        ToDate = trip.ToDate,
        Note = trip.Note,
        Selected = false
      };

    public static IEnumerable<TripDto> TripsToDto(IEnumerable<Trip> trips) =>
      trips.Select(trip => TripToDto(trip)).ToList();

    // Person
    public static PersonDto PersonToDto(Person person) =>
      new()
      {
        HashedPassword = person.HashedPassword,
        PersonName = person.PersonName,
        IsAdmin = person.IsAdmin
      };

    // Budget
    public static BudgetDto BudgetToDto(Budget budget) =>
      new()
      {
        Id = budget.Id,
        BudgetName = budget.BudgetName
      };

    public static IEnumerable<BudgetDto> BudgetsToDto(IEnumerable<Budget> budgets) =>
      budgets.Select(budget => BudgetToDto(budget)).ToList();

    // Unit
    public static UnitDto UnitToDto(Unit unit) =>
      new()
      {
        Id = unit.Id,
        UnitName = unit.UnitName,
        UseCurrency = unit.UseCurrency,
        Selected = false
      };

    public static IEnumerable<UnitDto> UnitsToDto(IEnumerable<Unit> units) =>
      units.Select(unit => UnitToDto(unit)).ToList();

    // Category
    public static CategoryDto CategoryToDto(Category category) =>
      new()
      {
        Id = category.Id,
        CategoryName = category.CategoryName,
        Selected = false
      };

    public static IEnumerable<CategoryDto> CategoriesToDto(IEnumerable<Category> categories) =>
      categories.Select(category => CategoryToDto(category)).ToList();

    // Budget years
    public static BudgetYearDto BudgetYearToDto(BudgetYear budgetYear) =>
      new()
      {
        Id = budgetYear.Id,
        Year = budgetYear.Year,
        BudgetId = budgetYear.BudgetId,
      };

    public static IEnumerable<BudgetYearDto> BudgetYearsToDto(IEnumerable<BudgetYear> budgetYears) =>
      budgetYears.Select(budgetYear => BudgetYearToDto(budgetYear)).ToList();

    public static ManageBudgetYear ToManageBudgetYear(IEnumerable<BudgetYearDto> budgetYears) =>
      new()
      {
        BudgetYears = budgetYears,
        Copy = budgetYears.Any(),
      };

    // Budget version
    public static BudgetVersionDto BudgetVersionToDto(BudgetVersion budgetVersion) =>
      new()
      {
        Id = budgetVersion.Id,
        BudgetYearId = budgetVersion.BudgetYearId,
        VersionName = budgetVersion.VersionName,
        DateCreated = budgetVersion.DateCreated,
        IsClosed = budgetVersion.IsClosed
      };

    public static IEnumerable<BudgetVersionDto> BudgetVersionsToDto(IEnumerable<BudgetVersion> budgetVersions) =>
      budgetVersions.Select(budgetVersion => BudgetVersionToDto(budgetVersion)).ToList();

    // Currency
    public static CurrencyDto CurrencyToDto(Currency currency) =>
      new()
      {
        Id = currency.Id,
        VersionId = currency.VersionId,
        Code = currency.Code,
        BudgetRate = currency.BudgetRate,
        AverageRate = currency.AverageRate,
        Selected = false
      };

    public static IEnumerable<CurrencyDto> CurrenciesToDto(IEnumerable<Currency> currencies) =>
      currencies.Select(c => CurrencyToDto(c)).ToList();

    // Budget item
    public static BudgetItemDto BudgetItemToDto(BudgetItem budgetItem) =>
      new()
      {
        Id = budgetItem.Id,
        VersionId = budgetItem.VersionId,
        CategoryId = budgetItem.CategoryId,
        UnitId = budgetItem.UnitId,
        CurrencyCode = budgetItem.CurrencyCode,
        UnitValue = budgetItem.UnitValue,
        Note = budgetItem.Note,
      };

    public static IEnumerable<BudgetItemDto> BudgetItemsToDto(IEnumerable<BudgetItem> budgetItems) =>
      budgetItems.Select(budgetItem => BudgetItemToDto(budgetItem)).ToList();

    // Actual item
    public static ActualItemDto ActualItemToDto(ActualItem actualItem) =>
      new()
      {
        Id = actualItem.Id,
        BudgetId = actualItem.BudgetId,
        CategoryId = actualItem.CategoryId,
        TripId = actualItem.TripId,
        PurchaseDate = actualItem.PurchaseDate,
        CurrencyCode = actualItem.CurrencyCode,
        Amount = actualItem.Amount,
        Note = actualItem.Note
      };

    public static IEnumerable<ActualItemDto> ActualItemsToDto(IEnumerable<ActualItem> actualItems) =>
      actualItems.Select(actualItem => ActualItemToDto(actualItem)).ToList();
  }
}