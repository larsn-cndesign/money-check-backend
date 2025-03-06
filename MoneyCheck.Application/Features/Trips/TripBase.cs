namespace MoneyCheck.Application.Features.Trips
{
  public abstract class TripBase
  {
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string? Note { get; set; }
  }
}