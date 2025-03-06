using MoneyCheck.Application.Contracts;

namespace MoneyCheck.Application.Features.Currencies
{
  public class CurrencyDto : ISelectable
  {
    public int Id { get; set; }

    public int VersionId { get; set; }

    public string Code { get; set; } = string.Empty;

    public decimal BudgetRate { get; set; }

    public decimal AverageRate { get; set; }

    public bool Selected { get; set; }
  }
}