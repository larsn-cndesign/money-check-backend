using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class Unit : AuditableEntity
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }

    public string UnitName { get; set; } = string.Empty;

    public bool UseCurrency { get; set; }

    // Navigation properties
    public Budget? Budget { get; set; }
    public ICollection<BudgetItem> BudgetItems { get; set; } = [];
  }
}