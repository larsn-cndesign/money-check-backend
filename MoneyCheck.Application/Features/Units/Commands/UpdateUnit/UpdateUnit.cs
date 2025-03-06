using MediatR;

namespace MoneyCheck.Application.Features.Units.Commands.UpdateUnit
{
  public class UpdateUnit : UnitBase, IRequest<UnitDto>
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }
  }
}