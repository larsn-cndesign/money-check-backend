using MoneyCheck.Application.Features.BudgetItems;
using MoneyCheck.Application.Features.BudgetVersions;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.Categories;
using MoneyCheck.Application.Features.Currencies;
using MoneyCheck.Application.Features.Units;

namespace MoneyCheck.Application.Models.Enteties
{
  public class ManageBudgetItem
  {
    public ItemFilter Filter { get; set; } = new();

    public BudgetVersionDto Version { get; set; } = new();

    public IEnumerable<BudgetItemDto> BudgetItems { get; set; } = [];

    public IEnumerable<CurrencyDto> Currencies { get; set; } = [];

    public IEnumerable<BudgetYearDto> BudgetYears { get; set; } = [];

    public IEnumerable<CategoryDto> Categories { get; set; } = [];

    public IEnumerable<UnitDto> Units { get; set; } = [];
  }
}