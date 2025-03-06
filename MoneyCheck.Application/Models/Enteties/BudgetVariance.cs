using MoneyCheck.Application.Features.BudgetVersions;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.Categories;
using MoneyCheck.Application.Features.Currencies;

namespace MoneyCheck.Application.Models.Enteties
{
  public class BudgetVariance
  {
    public ItemFilter Filter { get; set; } = new ItemFilter();

    public BudgetVersionDto Version { get; set; } = new();

    public CurrencyDto Currency { get; set; } = new();

    public VarianceItem VarianceItem { get; set; } = new();

    public IEnumerable<BudgetYearDto> BudgetYears { get; set; } = [];

    public IEnumerable<BudgetVersionDto> Versions { get; set; } = [];

    public IEnumerable<CategoryDto> Categories { get; set; } = [];

    public IEnumerable<CurrencyDto> Currencies { get; set; } = [];

    public IEnumerable<VarianceItem> VarianceItems { get; set; } = [];
  }
}