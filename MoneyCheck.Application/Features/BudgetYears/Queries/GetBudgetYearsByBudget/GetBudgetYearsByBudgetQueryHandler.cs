using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Application.Features.BudgetYears.Queries.GetBudgetYearsByBudget
{
  public class GetBudgetYearsByBudgetQueryHandler(ISharedRepository sharedRepository) :
    IRequestHandler<GetBudgetYearsByBudgetQuery, ManageBudgetYear>
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<ManageBudgetYear> Handle(GetBudgetYearsByBudgetQuery request, CancellationToken cancellationToken)
    {
      var budgetYears = await _sharedRepository.GetBudgetYearsByBudgetId(request.Id);

      var budgetYearsDto = EntityMapper.BudgetYearsToDto(budgetYears);

      return EntityMapper.ToManageBudgetYear(budgetYearsDto);
    }
  }
}