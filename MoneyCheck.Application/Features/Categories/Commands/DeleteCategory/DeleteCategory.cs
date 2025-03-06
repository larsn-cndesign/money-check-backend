using MediatR;

namespace MoneyCheck.Application.Features.Categories.Commands.DeleteCategory
{
  public class DeleteCategory : IRequest<CategoryDto>
  {
    public int Id { get; set; }
  }
}