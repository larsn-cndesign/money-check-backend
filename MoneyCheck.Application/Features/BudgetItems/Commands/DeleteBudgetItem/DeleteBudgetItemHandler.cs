using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.BudgetItems.Commands.DeleteBudgetItem
{
  public class DeleteBudgetItemHandler(IBudgetItemRepository budgetItemRepository) : IRequestHandler<DeleteBudgetItem>
  {
    private readonly IBudgetItemRepository _budgetItemRepository = budgetItemRepository;

    public async Task Handle(DeleteBudgetItem request, CancellationToken cancellationToken)
    {
      var budgetItemToDelete = await _budgetItemRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.BudgetItem, "BudgetItem", request.Id);

      await _budgetItemRepository.DeleteAsync(budgetItemToDelete);
    }
  }
}