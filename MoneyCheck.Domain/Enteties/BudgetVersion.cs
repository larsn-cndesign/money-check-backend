using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class BudgetVersion : AuditableEntity
  {
    public int Id { get; set; }

    public int BudgetYearId { get; set; }

    public string VersionName { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public bool IsClosed { get; set; }

    // Navigation properties
    public BudgetYear? BudgetYear { get; set; }
    public ICollection<BudgetItem> BudgetItems { get; set; } = [];
    public ICollection<Currency> Currencies { get; set; } = [];
  }
}