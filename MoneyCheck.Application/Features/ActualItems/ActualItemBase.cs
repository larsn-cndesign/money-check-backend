namespace MoneyCheck.Application.Features.ActualItems
{
  public abstract class ActualItemBase
  {
    public int CategoryId { get; set; }

    public int? TripId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public string CurrencyCode { get; set; } = "";

    public decimal Amount { get; set; }

    public string? Note { get; set; }
  }
}

