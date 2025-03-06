using MoneyCheck.Application.Contracts;

namespace MoneyCheck.Application.Models.Enteties
{
  public class VarianceItem : ISelectable
  {
    public string Category { get; set; } = string.Empty;

    public decimal Budget { get; set; } = decimal.Zero;

    public decimal Actual { get; set; } = decimal.Zero;

    public decimal Variance { get; set; } = decimal.Zero;

    public bool Selected { get; set; }
  }
}