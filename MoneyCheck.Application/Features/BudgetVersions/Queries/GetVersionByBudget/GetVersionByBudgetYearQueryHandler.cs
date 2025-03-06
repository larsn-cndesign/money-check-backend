using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Application.Features.BudgetVersions.Queries.GetVersionByBudget
{
  public class GetBudgetVersionsByBudgetYearQueryHandler(IBudgetVersionRepository budgetVersionRepository) :
    IRequestHandler<GetVersionByBudgetYearQuery, ManageBudgetYear>
  {
    private readonly IBudgetVersionRepository _budgetVersionRepository = budgetVersionRepository;

    public async Task<ManageBudgetYear> Handle(GetVersionByBudgetYearQuery request, CancellationToken cancellationToken)
    {
      return await _budgetVersionRepository.GetCurrentVersion(request.Id);
    }
  }
}