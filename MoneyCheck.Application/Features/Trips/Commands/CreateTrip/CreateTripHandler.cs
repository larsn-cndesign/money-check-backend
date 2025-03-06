using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Features.Trips.Commands.CreateTrip
{
  public class CreateTripHandler(ITripRepository tripRepository) : IRequestHandler<CreateTrip, TripDto>
  {
    private readonly ITripRepository _tripRepository = tripRepository;

    public async Task<TripDto> Handle(CreateTrip request, CancellationToken cancellationToken)
    {
      var validator = new TripCommandValidator(_tripRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      var trip = new Trip
      {
        Id = 0,
        BudgetId = request.BudgetId,
        FromDate = request.FromDate,
        ToDate = request.ToDate,
        Note = request.Note,
      };
      trip = await _tripRepository.AddAsync(trip);

      return EntityMapper.TripToDto(trip);
    }
  }
}