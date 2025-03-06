using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class Category : AuditableEntity
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    // Navigation properties
    public Budget? Budget { get; set; }
    public ICollection<ActualItem> ActualItems { get; set; } = [];
    public ICollection<BudgetItem> BudgetItems { get; set; } = [];
  }
}