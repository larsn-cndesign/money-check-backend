namespace MoneyCheck.Application.Models.Enteties
{
  public class ItemFilter
  {
    public int BudgetId { get; set; } = -1;

    public int VersionId { get; set; } = -1;

    public int BudgetYearId { get; set; } = -1;

    public int CategoryId { get; set; } = -1;

    public int TripId { get; set; } = -1;

    public string CurrencyCode { get; set; } = string.Empty;

    public string? Note { get; set; }

    public List<FilterList> List { get; set; } = [];

    public int Month { get; set; } = -1;

    public bool TravelDay { get; set; } = false;

    public int TravelDayCount { get; set; } = 1;
  }
}