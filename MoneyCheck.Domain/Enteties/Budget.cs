using MoneyCheck.Domain.Common;

namespace MoneyCheck.Domain.Enteties
{
  public class Budget : AuditableEntity
  {
    public int Id { get; set; }

    public string BudgetName { get; set; } = "";

    // Navigation properties
    public ICollection<ActualItem> ActualItems { get; set; } = [];
    public ICollection<BudgetYear> BudgetYears { get; set; } = [];
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Trip> Trips { get; set; } = [];
    public ICollection<Unit> Units { get; set; } = [];
  }
}