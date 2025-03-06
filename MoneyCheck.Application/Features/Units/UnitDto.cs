using MoneyCheck.Application.Contracts;

namespace MoneyCheck.Application.Features.Units
{
  public class UnitDto : UnitBase, ISelectable
  {
    public int Id { get; set; }

    public bool Selected { get; set; }
  }
}