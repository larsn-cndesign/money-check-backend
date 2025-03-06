using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.BudgetVersions.Commands.DeleteBudgetVersion
{
  public class DeleteBudgetVersionHandler(IBudgetVersionRepository budgetVersionRepository) : IRequestHandler<DeleteBudgetVersion>
  {
    private readonly IBudgetVersionRepository _budgetVersionRepository = budgetVersionRepository;

    public async Task Handle(DeleteBudgetVersion request, CancellationToken cancellationToken)
    {
      await _budgetVersionRepository.DeleteBudgetVersionAsync(request.Id);
    }
  }
}