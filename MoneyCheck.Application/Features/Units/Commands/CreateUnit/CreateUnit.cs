using MediatR;

namespace MoneyCheck.Application.Features.Units.Commands.CreateUnit
{
  public class CreateUnit : UnitBase, IRequest<UnitDto>
  {
    public int BudgetId { get; set; }
  }
}