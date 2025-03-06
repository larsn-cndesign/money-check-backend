using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class Currency : AuditableEntity
  {
    public int Id { get; set; }

    public int VersionId { get; set; }

    public string Code { get; set; } = "";

    public decimal BudgetRate { get; set; }

    public decimal AverageRate { get; set; }

    // Navigation properties
    public BudgetVersion? BudgetVersion { get; set; }
  }
}