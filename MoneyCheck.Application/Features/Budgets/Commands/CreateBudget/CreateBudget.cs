using MediatR;

namespace MoneyCheck.Application.Features.Budgets.Commands.CreateBudget
{
  public class CreateBudget : BudgetBase, IRequest<BudgetDto>
  {
  }
}