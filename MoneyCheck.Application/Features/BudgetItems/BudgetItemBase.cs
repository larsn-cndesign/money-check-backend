namespace MoneyCheck.Application.Features.BudgetItems
{
  public abstract class BudgetItemBase
  {
    public int CategoryId { get; set; }

    public int UnitId { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public decimal UnitValue { get; set; }

    public string? Note { get; set; }
  }
}