using MediatR;
using MoneyCheck.Application.Features.Currencies;

namespace MoneyCheck.Application.Features.BudgetVersions.Commands.CreateBudgetVersion
{
  public class CreateBudgetVersion : BudgetVersionBase, IRequest
  {
    public int BudgetYearId { get; set; }

    public bool Copy { get; set; }

    public IEnumerable<CurrencyDto> Currencies { get; set; } = [];
  }
}

