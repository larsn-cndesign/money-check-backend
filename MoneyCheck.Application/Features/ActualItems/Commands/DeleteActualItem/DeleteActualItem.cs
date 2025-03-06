using MediatR;

namespace MoneyCheck.Application.Features.ActualItems.Commands.DeleteActualItem
{
  public class DeleteActualItem : IRequest
  {
    public int Id { get; set; }
  }
}