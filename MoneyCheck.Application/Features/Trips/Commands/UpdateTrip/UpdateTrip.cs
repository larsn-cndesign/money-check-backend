using MediatR;

namespace MoneyCheck.Application.Features.Trips.Commands.UpdateTrip
{
  public class UpdateTrip : TripBase, IRequest<TripDto>
  {
    public int Id { get; set; }
  }
}