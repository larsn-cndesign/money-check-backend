using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Units.Commands.DeleteUnit
{
  public class DeleteUnitHandler(IUnitRepository unitRepository) : IRequestHandler<DeleteUnit, UnitDto>
  {
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<UnitDto> Handle(DeleteUnit request, CancellationToken cancellationToken)
    {
      var unitToDelete = await _unitRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.Unit, "Unit", request.Id);

      await _unitRepository.DeleteAsync(unitToDelete);

      return EntityMapper.UnitToDto(unitToDelete);
    }
  }
}