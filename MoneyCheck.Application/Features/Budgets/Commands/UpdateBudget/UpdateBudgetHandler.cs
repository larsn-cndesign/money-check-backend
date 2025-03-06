using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Budgets.Commands.UpdateBudget
{
  public class UpdateBudgetHandler(IBudgetRepository budgetRepository) : IRequestHandler<UpdateBudget, BudgetDto>
  {
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task<BudgetDto> Handle(UpdateBudget request, CancellationToken cancellationToken)
    {
      var budgetToUpdate = await _budgetRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.Budget, "Budget", request.Id);

      budgetToUpdate.BudgetName = request.BudgetName;
      await _budgetRepository.UpdateAsync(budgetToUpdate);

      return EntityMapper.BudgetToDto(budgetToUpdate);
    }
  }
}