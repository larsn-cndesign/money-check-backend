using MediatR;
using MoneyCheck.Application.Models.Enteties;

namespace MoneyCheck.Application.Features.BudgetYears.Queries.GetBudgetYearsByBudget
{
  public class GetBudgetYearsByBudgetQuery : IRequest<ManageBudgetYear>
  {
    public int Id { get; set; }
  }
}