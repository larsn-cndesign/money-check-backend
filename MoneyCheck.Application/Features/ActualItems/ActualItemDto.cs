using MoneyCheck.Application.Contracts;

namespace MoneyCheck.Application.Features.ActualItems
{
  public class ActualItemDto : ActualItemBase, ISelectable
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }

    public bool Selected { get; set; } = false;

    public string CategoryName { get; set; } = "";

    public string TripName { get; set; } = "";
  }
}