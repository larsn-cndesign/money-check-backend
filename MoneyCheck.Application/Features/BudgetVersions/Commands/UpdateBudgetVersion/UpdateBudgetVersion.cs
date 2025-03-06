using MediatR;
using MoneyCheck.Application.Features.BudgetYears;
using MoneyCheck.Application.Features.Currencies;

namespace MoneyCheck.Application.Features.BudgetVersions.Commands.UpdateBudgetVersion
{
  public class UpdateBudgetVersion : BudgetVersionBase, IRequest
  {
    public BudgetVersionDto Version { get; set; } = new();

    public IEnumerable<CurrencyDto> Currencies { get; set; } = [];
  }
}