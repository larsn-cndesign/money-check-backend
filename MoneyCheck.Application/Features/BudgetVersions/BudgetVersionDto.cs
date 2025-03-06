using MoneyCheck.Application.Contracts;

namespace MoneyCheck.Application.Features.BudgetVersions
{
  public class BudgetVersionDto : BudgetVersionBase
  {
    public int Id { get; set; }

    public int BudgetYearId { get; set; }

    public string VersionName { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public bool IsClosed { get; set; }
  }
}

