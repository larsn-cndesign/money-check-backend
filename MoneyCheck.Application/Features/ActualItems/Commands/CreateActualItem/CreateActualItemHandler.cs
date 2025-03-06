using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;

namespace MoneyCheck.Application.Features.ActualItems.Commands.CreateActualItem
{
  public class CreateActualItemHandler(IActualItemRepository actualItemRepository) : IRequestHandler<CreateActualItem, ActualItemDto>
  {
    private readonly IActualItemRepository _actualItemRepository = actualItemRepository;

    public async Task<ActualItemDto> Handle(CreateActualItem request, CancellationToken cancellationToken)
    {
      var validator = new ActualItemCommandValidator(_actualItemRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      var actualItemDto = new ActualItemDto
      {
        BudgetId = request.BudgetId,
        CategoryId = request.CategoryId,
        TripId = request.TripId,
        PurchaseDate = request.PurchaseDate,
        CurrencyCode = request.CurrencyCode,
        Amount = request.Amount,
        Note = request.Note,
      };

      return await _actualItemRepository.AddActualItemAsync(actualItemDto);
    }
  }
}