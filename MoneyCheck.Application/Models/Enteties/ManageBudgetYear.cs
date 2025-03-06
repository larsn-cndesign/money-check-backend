using MoneyCheck.Application.Features.BudgetVersions;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.Currencies;

namespace MoneyCheck.Application.Models.Enteties
{
  public class ManageBudgetYear
  {
    public bool Copy { get; set; }

    public BudgetYearDto BudgetYear { get; set; } = new();

    public BudgetVersionDto Version { get; set; } = new();

    public IEnumerable<CurrencyDto> Currencies { get; set; } = [];

    public IEnumerable<BudgetVersionDto> Versions { get; set; } = [];

    public IEnumerable<BudgetYearDto> BudgetYears { get; set; } = [];
  }
}