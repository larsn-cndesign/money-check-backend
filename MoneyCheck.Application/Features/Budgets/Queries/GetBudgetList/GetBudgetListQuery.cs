using MediatR;

namespace MoneyCheck.Application.Features.Budgets.Queries.GetBudgetList
{
  public class GetBudgetListQuery : IRequest<IEnumerable<BudgetDto>>
  {
    public int Id { get; set; }
  }
}