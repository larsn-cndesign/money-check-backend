using FluentValidation;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Units.Commands
{
  public class UnitCommandValidator : AbstractValidator<UnitBase>
  {
    private readonly IUnitRepository _unitRepository; // TODO Remove if not used

    public UnitCommandValidator(IUnitRepository unitRepository)
    {
      _unitRepository = unitRepository;

      ClassLevelCascadeMode = CascadeMode.Stop;
      RuleLevelCascadeMode = CascadeMode.Stop;

      // TODO More validations
      RuleFor(p => p.UnitName)
        .NotEmpty().WithMessage(new LocaleError(LocaleErrorKey.Required, [LocaleErrorParam.Unit]).ToJson());
    }
  }
}