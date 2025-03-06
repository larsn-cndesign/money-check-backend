using MediatR;

namespace MoneyCheck.Application.Features.Trips.Commands.DeleteTrip
{
  public class DeleteTrip : IRequest<TripDto>
  {
    public int Id { get; set; }
  }
}