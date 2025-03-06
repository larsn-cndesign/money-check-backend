using MediatR;

namespace MoneyCheck.Application.Features.BudgetItems.Commands.CreateBudgetItem
{
  public class CreateBudgetItem : BudgetItemBase, IRequest<BudgetItemDto>
  {
    public int VersionId { get; set; }
  }
}