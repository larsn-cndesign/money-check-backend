namespace MoneyCheck.Application.Features.Units
{
  public abstract class UnitBase
  {
    public string UnitName { get; set; } = string.Empty;

    public bool UseCurrency { get; set; }
  }
}