using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Features.Budgets.Queries.GetBudgetList
{
  public class GetBudgetListQueryHandler(IAsyncRepository<Budget> budgetRepository) :
    IRequestHandler<GetBudgetListQuery, IEnumerable<BudgetDto>>
  {
    private readonly IAsyncRepository<Budget> _budgetRepository = budgetRepository;

    public async Task<IEnumerable<BudgetDto>> Handle(GetBudgetListQuery request, CancellationToken cancellationToken)
    {
      var budgets = await _budgetRepository.ListAllAsync();

      return EntityMapper.BudgetsToDto([.. budgets]);
    }
  }
}