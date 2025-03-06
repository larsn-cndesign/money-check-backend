using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Features.BudgetVersions.Commands.CreateBudgetVersion
{
  public class CreateBudgetVersionHandler(IBudgetVersionRepository budgetVersionRepository) : IRequestHandler<CreateBudgetVersion>
  {
    private readonly IBudgetVersionRepository _budgetVersionRepository = budgetVersionRepository;

    public async Task Handle(CreateBudgetVersion request, CancellationToken cancellationToken)
    {
      var validator = new BudgetVersionCommandValidator(_budgetVersionRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      await _budgetVersionRepository.AddBudgetVersionAsync(request);
    }
  }
}