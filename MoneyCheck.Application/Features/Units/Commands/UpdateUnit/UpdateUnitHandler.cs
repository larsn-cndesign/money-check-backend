using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Units.Commands.UpdateUnit
{
  public class UpdateUnitHandler(IUnitRepository unitRepository) : IRequestHandler<UpdateUnit, UnitDto>
  {
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<UnitDto> Handle(UpdateUnit request, CancellationToken cancellationToken)
    {
      var unitToUpdate = await _unitRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.Unit, "Unit", request.Id);

      var validator = new UnitCommandValidator(_unitRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      unitToUpdate.UnitName = request.UnitName;
      unitToUpdate.UseCurrency = request.UseCurrency;

      await _unitRepository.UpdateAsync(unitToUpdate);

      return EntityMapper.UnitToDto(unitToUpdate);
    }
  }
}