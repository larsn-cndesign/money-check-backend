using MediatR;

namespace MoneyCheck.Application.Features.BudgetVersions.Commands.DeleteBudgetVersion
{
  public class DeleteBudgetVersion : IRequest
  {
    public int Id { get; set; }
  }
}

