namespace MoneyCheck.Application.Features.Budgets.Queries.GetBudgetState
{
  public class BudgetState
  {
    public int BudgetId { get; set; } = -1;
    public string BudgetName { get; set; } = "";
    public bool HasBudget { get; set; } = false;

    public IEnumerable<BudgetDto> Budgets { get; set; } = [];
  }
}