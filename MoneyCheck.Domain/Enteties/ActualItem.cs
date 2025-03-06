using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class ActualItem : AuditableEntity
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }

    public int CategoryId { get; set; }

    public int? TripId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string? Note { get; set; }

    // Navigation properties
    public Budget? Budget { get; set; }
    public Category? Category { get; set; }
    public Trip? Trip { get; set; }
  }
}