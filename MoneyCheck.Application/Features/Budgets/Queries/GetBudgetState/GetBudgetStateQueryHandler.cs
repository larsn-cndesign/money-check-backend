using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Features.Budgets.Queries.GetBudgetState
{
  public class GetBudgetStateQueryHandler(IAsyncRepository<Budget> budgetRepository) :
    IRequestHandler<GetBudgetStateQuery, BudgetState>
  {
    private readonly IAsyncRepository<Budget> _budgetRepository = budgetRepository;

    public async Task<BudgetState> Handle(GetBudgetStateQuery request, CancellationToken cancellationToken)
    {
      var budgets = await _budgetRepository.ListAllAsync();

      var firstBudget = budgets.FirstOrDefault();

      var budgetState = new BudgetState
      {
        Budgets = EntityMapper.BudgetsToDto(budgets),
        BudgetId = firstBudget?.Id ?? -1,
        BudgetName = firstBudget?.BudgetName ?? "",
        HasBudget = firstBudget != null
      };

      return budgetState;
    }
  }
}