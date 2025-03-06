using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.ActualItems.Commands.UpdateActualItem
{
  public class UpdateActualItemHandler(IActualItemRepository actualItemRepository) : IRequestHandler<UpdateActualItem>
  {
    private readonly IActualItemRepository _actualItemRepository = actualItemRepository;

    public async Task Handle(UpdateActualItem request, CancellationToken cancellationToken)
    {
      var validator = new ActualItemCommandValidator(_actualItemRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      var actualItemDto = new ActualItemDto
      {
        Id = request.Id,
        CategoryId = request.CategoryId,
        TripId = request.TripId,
        PurchaseDate = request.PurchaseDate,
        CurrencyCode = request.CurrencyCode,
        Amount = request.Amount,
        Note = request.Note,
      };

      await _actualItemRepository.UpdateActualItem(actualItemDto);
    }
  }
}