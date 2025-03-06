using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class BudgetItem : AuditableEntity
  {
    public int Id { get; set; }

    public int VersionId { get; set; }

    public int CategoryId { get; set; }

    public int UnitId { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public decimal UnitValue { get; set; }

    public string? Note { get; set; }

    // Navigation properties
    public Category? Category { get; set; }
    public Unit? Unit { get; set; }
    public BudgetVersion? BudgetVersion { get; set; }
  }
}