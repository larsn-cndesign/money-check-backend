using MediatR;

namespace MoneyCheck.Application.Features.Categories.Queries.GetCategoriesByBudget
{
  public class GetCategoriesByBudgetQuery : IRequest<IEnumerable<CategoryDto>>
  {
    public int Id { get; set; }
  }
}