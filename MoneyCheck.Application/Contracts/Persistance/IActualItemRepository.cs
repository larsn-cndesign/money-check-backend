using MoneyCheck.Application.Features.ActualItems;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Contracts.Persistance
{
  public interface IActualItemRepository : IAsyncRepository<ActualItem>
  {
    Task<ManageActualItem> GetActualItems(ItemFilter filter);

    Task<ActualItemDto> AddActualItemAsync(ActualItemDto actualItem);

    Task UpdateActualItem(ActualItemDto actualItem);
  }
}