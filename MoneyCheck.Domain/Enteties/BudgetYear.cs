using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class BudgetYear : AuditableEntity
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }

    public int Year { get; set; }

    // Navigation properties
    public Budget? Budget { get; set; }
    public ICollection<BudgetVersion> BudgetVersions { get; set; } = [];
  }
}