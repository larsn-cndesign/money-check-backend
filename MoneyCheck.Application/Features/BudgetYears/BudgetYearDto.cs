namespace MoneyCheck.Application.Features.BudgetYears
{
  public class BudgetYearDto : BudgetYearBase
  {
    public int Id { get; set; }

    public int BudgetId { get; set; }
  }
}