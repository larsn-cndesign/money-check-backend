using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;

namespace MoneyCheck.Application.Features.BudgetItems.Commands.UpdateBudgetItem
{
  public class UpdateBudgetItemHandler(IBudgetItemRepository budgetItemRepository) : IRequestHandler<UpdateBudgetItem>
  {
    private readonly IBudgetItemRepository _budgetItemRepository = budgetItemRepository;

    public async Task Handle(UpdateBudgetItem request, CancellationToken cancellationToken)
    {
      var validator = new BudgetItemCommandValidator(_budgetItemRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      var budgetItemDto = new BudgetItemDto
      {
        Id = request.Id,
        CategoryId = request.CategoryId,
        UnitId = request.UnitId,
        CurrencyCode = request.CurrencyCode,
        UnitValue = request.UnitValue,
        Note = request.Note,
      };

      await _budgetItemRepository.UpdateBudgetItem(budgetItemDto);
    }
  }
}