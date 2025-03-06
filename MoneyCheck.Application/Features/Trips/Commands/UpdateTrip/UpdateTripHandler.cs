using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Trips.Commands.UpdateTrip
{
  public class UpdateTripHandler(ITripRepository tripRepository) : IRequestHandler<UpdateTrip, TripDto>
  {
    private readonly ITripRepository _tripRepository = tripRepository;

    public async Task<TripDto> Handle(UpdateTrip request, CancellationToken cancellationToken)
    {
      var tripToUpdate = await _tripRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.Trip, "Trip", request.Id);

      var validator = new TripCommandValidator(_tripRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      tripToUpdate.FromDate = request.FromDate;
      tripToUpdate.ToDate = request.ToDate;
      tripToUpdate.Note = request.Note;

      await _tripRepository.UpdateAsync(tripToUpdate);

      return EntityMapper.TripToDto(tripToUpdate);
    }
  }
}