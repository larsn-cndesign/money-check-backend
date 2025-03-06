using MediatR;

namespace MoneyCheck.Application.Features.BudgetItems.Commands.DeleteBudgetItem
{
  public class DeleteBudgetItem : IRequest
  {
    public int Id { get; set; }
  }
}