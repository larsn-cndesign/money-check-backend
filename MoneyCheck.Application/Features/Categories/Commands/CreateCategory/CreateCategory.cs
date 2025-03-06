using MediatR;

namespace MoneyCheck.Application.Features.Categories.Commands.CreateCategory
{
  public class CreateCategory : CategoryBase, IRequest<CategoryDto>
  {
    public int BudgetId { get; set; }
  }
}