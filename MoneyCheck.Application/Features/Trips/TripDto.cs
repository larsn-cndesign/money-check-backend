using MoneyCheck.Application.Contracts;

namespace MoneyCheck.Application.Features.Trips
{
  public class TripDto : TripBase, ISelectable
  {
    public int Id { get; set; }

    public bool Selected { get; set; }
  }
}