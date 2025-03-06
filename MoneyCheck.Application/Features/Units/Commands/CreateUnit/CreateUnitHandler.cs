using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;

namespace MoneyCheck.Application.Features.Units.Commands.CreateUnit
{
  public class CreateUnitHandler(IUnitRepository unitRepository) : IRequestHandler<CreateUnit, UnitDto>
  {
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<UnitDto> Handle(CreateUnit request, CancellationToken cancellationToken)
    {
      var validator = new UnitCommandValidator(_unitRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      var unit = new Domain.Enteties.Unit
      {
        Id = 0,
        BudgetId = request.BudgetId,
        UnitName = request.UnitName,
        UseCurrency = request.UseCurrency,
      };
      unit = await _unitRepository.AddAsync(unit);

      return EntityMapper.UnitToDto(unit);
    }
  }
}