using MediatR;

namespace MoneyCheck.Application.Features.BudgetItems.Commands.UpdateBudgetItem
{
  public class UpdateBudgetItem : BudgetItemBase, IRequest
  {
    public int Id { get; set; }
  }
}