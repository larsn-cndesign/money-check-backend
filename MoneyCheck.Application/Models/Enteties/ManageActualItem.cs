using MoneyCheck.Application.Features.ActualItems;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.Categories;
using MoneyCheck.Application.Features.Trips;

namespace MoneyCheck.Application.Models.Enteties
{
  public class ManageActualItem
  {
    public ItemFilter Filter { get; set; } = new ItemFilter();

    public IEnumerable<BudgetYearDto> BudgetYears { get; set; } = [];

    public IEnumerable<ActualItemDto> ActualItems { get; set; } = [];

    public IEnumerable<CategoryDto> Categories { get; set; } = [];

    public IEnumerable<TripDto> Trips { get; set; } = [];

    public IEnumerable<CurrencyItem> CurrencyItems { get; set; } = [];

    public IEnumerable<string> Currencies { get; set; } = [];
  }
}