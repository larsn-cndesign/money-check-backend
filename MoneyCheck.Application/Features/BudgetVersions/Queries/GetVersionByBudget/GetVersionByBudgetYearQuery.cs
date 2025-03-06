using MediatR;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Application.Features.BudgetVersions.Queries.GetVersionByBudget
{
  public class GetVersionByBudgetYearQuery : IRequest<ManageBudgetYear>
  {
    public int Id { get; set; }
  }
}