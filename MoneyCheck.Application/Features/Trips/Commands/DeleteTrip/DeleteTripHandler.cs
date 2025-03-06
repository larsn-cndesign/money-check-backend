using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Trips.Commands.DeleteTrip
{
  public class DeleteTripHandler(ITripRepository tripRepository) : IRequestHandler<DeleteTrip, TripDto>
  {
    private readonly ITripRepository _tripRepository = tripRepository;

    public async Task<TripDto> Handle(DeleteTrip request, CancellationToken cancellationToken)
    {
      var tripToDelete = await _tripRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.Trip, "Trip", request.Id);

      await _tripRepository.DeleteAsync(tripToDelete);

      return EntityMapper.TripToDto(tripToDelete);
    }
  }
}