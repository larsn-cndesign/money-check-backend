using MediatR;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Application.Features.ActualItems.Queries.GetActualItem
{
  public class GetActualItemQuery : IRequest<ManageActualItem>
  {
    public ItemFilter Filter { get; set; } = new();
  }
}

