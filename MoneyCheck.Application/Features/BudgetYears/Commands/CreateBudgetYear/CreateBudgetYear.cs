using MediatR;
using MoneyCheck.Application.Features.Currencies;

namespace MoneyCheck.Application.Features.BudgetYears.Commands.CreateBudgetYear
{
  public class CreateBudgetYear : BudgetYearBase, IRequest<BudgetYearDto>
  {
    public int BudgetId { get; set; }

    public bool Copy { get; set; }

    public IEnumerable<CurrencyDto> Currencies { get; set; } = [];
  }
}