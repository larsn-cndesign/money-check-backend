using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Enteties;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.BudgetItems.Queries.GetBudgetItem
{
  public class GetBudgetItemQueryHandler(IBudgetItemRepository budgetItemRepository) :
    IRequestHandler<GetBudgetItemQuery, ManageBudgetItem>
  {
    private readonly IBudgetItemRepository _budgetItemRepository = budgetItemRepository;

    public async Task<ManageBudgetItem> Handle(GetBudgetItemQuery request, CancellationToken cancellationToken)
    {
      if (request.Filter == null)
        throw new BadRequestException(new LocaleError(LocaleErrorKey.NotFound, LocaleErrorKey.InvalidFilter).ToJson());

      return await _budgetItemRepository.GetBudgetItems(request.Filter);
    }
  }
}