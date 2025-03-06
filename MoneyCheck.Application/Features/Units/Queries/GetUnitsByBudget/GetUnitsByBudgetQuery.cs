using MediatR;

namespace MoneyCheck.Application.Features.Units.Queries.GetUnitsByBudget
{
  public class GetUnitsByBudgetQuery : IRequest<IEnumerable<UnitDto>>
  {
    public int Id { get; set; }
  }
}