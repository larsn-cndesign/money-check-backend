using MediatR;

namespace MoneyCheck.Application.Features.ActualItems.Commands.CreateActualItem
{
  public class CreateActualItem : ActualItemBase, IRequest<ActualItemDto>
  {
    public int BudgetId { get; set; }
  }
}

