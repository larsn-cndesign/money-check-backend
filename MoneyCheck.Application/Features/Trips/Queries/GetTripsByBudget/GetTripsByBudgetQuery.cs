using MediatR;

namespace MoneyCheck.Application.Features.Trips.Queries.GetTripsByBudget
{
  public class GetTripsByBudgetQuery : IRequest<IEnumerable<TripDto>>
  {
    public int Id { get; set; }
  }
}