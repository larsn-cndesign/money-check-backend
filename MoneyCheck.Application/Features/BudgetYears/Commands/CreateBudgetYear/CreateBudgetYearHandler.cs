using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;

namespace MoneyCheck.Application.Features.BudgetYears.Commands.CreateBudgetYear
{
  public class CreateBudgetYearHandler(IBudgetYearRepository budgetYearRepository) : IRequestHandler<CreateBudgetYear, BudgetYearDto>
  {
    private readonly IBudgetYearRepository _budgetYearRepository = budgetYearRepository;

    public async Task<BudgetYearDto> Handle(CreateBudgetYear request, CancellationToken cancellationToken)
    {
      var validator = new BudgetYearCommandValidator(_budgetYearRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      return await _budgetYearRepository.AddBudgetYearAsync(request);
    }
  }
}