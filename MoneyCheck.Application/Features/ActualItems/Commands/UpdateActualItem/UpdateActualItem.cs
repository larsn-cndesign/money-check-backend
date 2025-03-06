using MediatR;

namespace MoneyCheck.Application.Features.ActualItems.Commands.UpdateActualItem
{
  public class UpdateActualItem : ActualItemBase, IRequest
  {
    public int Id { get; set; }
  }
}