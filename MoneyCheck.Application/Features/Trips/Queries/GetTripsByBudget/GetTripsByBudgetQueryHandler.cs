using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Mapping;

namespace MoneyCheck.Application.Features.Trips.Queries.GetTripsByBudget
{
  public class GetTripsByBudgetQueryHandler(ISharedRepository sharedRepository) : IRequestHandler<GetTripsByBudgetQuery, IEnumerable<TripDto>>
  {
    private readonly ISharedRepository _sharedRepository = sharedRepository;

    public async Task<IEnumerable<TripDto>> Handle(GetTripsByBudgetQuery request, CancellationToken cancellationToken)
    {
      var trips = await _sharedRepository.GetTripsByBudgetId(request.Id);

      return EntityMapper.TripsToDto(trips);
    }
  }
}