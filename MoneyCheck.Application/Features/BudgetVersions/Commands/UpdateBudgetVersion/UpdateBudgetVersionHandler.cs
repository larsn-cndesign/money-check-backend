using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.BudgetVersions.Commands.UpdateBudgetVersion
{
  public class UpdateBudgetVersionHandler(IBudgetVersionRepository budgetVersionRepository) : IRequestHandler<UpdateBudgetVersion>
  {
    private readonly IBudgetVersionRepository _budgetVersionRepository = budgetVersionRepository;

    public async Task Handle(UpdateBudgetVersion request, CancellationToken cancellationToken)
    {
      var validator = new BudgetVersionCommandValidator(_budgetVersionRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      await _budgetVersionRepository.UpdateBudgetVersionAsync(request);
    }
  }
}