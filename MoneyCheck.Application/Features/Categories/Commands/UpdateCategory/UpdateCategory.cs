using MediatR;

namespace MoneyCheck.Application.Features.Categories.Commands.UpdateCategory
{
  public class UpdateCategory : CategoryBase, IRequest<CategoryDto>
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }
  }
}