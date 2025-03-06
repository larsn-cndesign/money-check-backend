using MediatR;

namespace MoneyCheck.Application.Features.Budgets.Commands.UpdateBudget
{
  public class UpdateBudget : BudgetBase, IRequest<BudgetDto>
  {
    public int Id { get; set; }
  }
}