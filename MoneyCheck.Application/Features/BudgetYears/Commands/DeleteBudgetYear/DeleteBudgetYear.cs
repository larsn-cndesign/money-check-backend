using MediatR;

namespace MoneyCheck.Application.Features.BudgetYears.Commands.DeleteBudgetYear
{
  public class DeleteBudgetYear : IRequest
  {
    public int Id { get; set; }
  }
}

