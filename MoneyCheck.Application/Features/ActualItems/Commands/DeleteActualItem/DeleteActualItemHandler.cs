using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.ActualItems.Commands.DeleteActualItem
{
  public class DeleteActualItemHandler(IActualItemRepository actualItemRepository) : IRequestHandler<DeleteActualItem>
  {
    private readonly IActualItemRepository _actualItemRepository = actualItemRepository;

    public async Task Handle(DeleteActualItem request, CancellationToken cancellationToken)
    {
      var actualItemToDelete = await _actualItemRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.ActualItem, "ActualItem", request.Id);

      await _actualItemRepository.DeleteAsync(actualItemToDelete);
    }
  }
}