using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class Trip : AuditableEntity
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string? Note { get; set; }

    // Navigation properties
    public Budget? Budget { get; set; }
    public ICollection<ActualItem> ActualItems { get; set; } = [];
  }
}