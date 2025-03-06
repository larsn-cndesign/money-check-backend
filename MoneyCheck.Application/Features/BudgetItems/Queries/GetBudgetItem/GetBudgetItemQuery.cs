using MediatR;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Application.Features.BudgetItems.Queries.GetBudgetItem
{
  public class GetBudgetItemQuery : IRequest<ManageBudgetItem>
  {
    public ItemFilter Filter { get; set; } = new();
  }
}