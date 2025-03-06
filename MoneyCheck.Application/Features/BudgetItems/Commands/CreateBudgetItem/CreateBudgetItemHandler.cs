using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;

namespace MoneyCheck.Application.Features.BudgetItems.Commands.CreateBudgetItem
{
  public class CreateBudgetItemHandler(IBudgetItemRepository budgetItemRepository) : IRequestHandler<CreateBudgetItem, BudgetItemDto>
  {
    private readonly IBudgetItemRepository _budgetItemRepository = budgetItemRepository;

    public async Task<BudgetItemDto> Handle(CreateBudgetItem request, CancellationToken cancellationToken)
    {
      var validator = new BudgetItemCommandValidator(_budgetItemRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      var budgetItemDto = new BudgetItemDto
      {
        VersionId = request.VersionId,
        CategoryId = request.CategoryId,
        UnitId = request.UnitId,
        CurrencyCode = request.CurrencyCode,
        UnitValue = request.UnitValue,
        Note = request.Note,
      };

      return await _budgetItemRepository.AddBudgetItemAsync(budgetItemDto);
    }
  }
}