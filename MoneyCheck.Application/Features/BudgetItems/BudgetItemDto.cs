using MoneyCheck.Application.Contracts;

namespace MoneyCheck.Application.Features.BudgetItems
{
  public class BudgetItemDto : BudgetItemBase, ISelectable
  {
    public int Id { get; set; }

    public int VersionId { get; set; }

    public bool Selected { get; set; } = false;

    public string CategoryName { get; set; } = string.Empty;

    public string UnitName { get; set; } = string.Empty;
  }
}