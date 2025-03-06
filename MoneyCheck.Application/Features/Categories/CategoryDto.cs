using MoneyCheck.Application.Contracts;

namespace MoneyCheck.Application.Features.Categories
{
  public class CategoryDto : CategoryBase, ISelectable
  {
    public int Id { get; set; }

    public bool Selected { get; set; } = false;
  }
}