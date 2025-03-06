using MediatR;
using MoneyCheck.Application.Contracts.Persistance;

namespace MoneyCheck.Application.Features.BudgetYears.Commands.DeleteBudgetYear
{
  public class DeleteBudgetYearHandler(IBudgetYearRepository budgetYearRepository) : IRequestHandler<DeleteBudgetYear>
  {
    private readonly IBudgetYearRepository _budgetYearRepository = budgetYearRepository;

    public async Task Handle(DeleteBudgetYear request, CancellationToken cancellationToken)
    {
      await _budgetYearRepository.DeleteBudgetYearAsync(request.Id);
    }
  }
}