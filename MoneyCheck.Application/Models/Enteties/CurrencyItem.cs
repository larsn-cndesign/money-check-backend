namespace MoneyCheck.Application.Models.Enteties
{
  public class CurrencyItem
  {
    public string CurrencyCode { get; set; } = string.Empty;

    public decimal BudgetRate { get; set; } = decimal.Zero;

    public decimal AverageRate { get; set; } = decimal.Zero;
  }
}