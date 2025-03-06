using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Features.Budgets.Commands.CreateBudget
{
  public class CreateBudgetHandler(IBudgetRepository budgetRepository) : IRequestHandler<CreateBudget, BudgetDto>
  {
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task<BudgetDto> Handle(CreateBudget request, CancellationToken cancellationToken)
    {
      var budget = new Budget
      {
        Id = 0,
        BudgetName = request.BudgetName
      };
      budget = await _budgetRepository.AddAsync(budget);

      return EntityMapper.BudgetToDto(budget);
    }
  }
}