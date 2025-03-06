using MediatR;

namespace MoneyCheck.Application.Features.Trips.Commands.CreateTrip
{
  public class CreateTrip : TripBase, IRequest<TripDto>
  {
    public int BudgetId { get; set; }
  }
}