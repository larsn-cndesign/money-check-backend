using FluentValidation;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Trips.Commands
{
  public class TripCommandValidator : AbstractValidator<TripBase>
  {
    private readonly ITripRepository _tripRepository;

    public TripCommandValidator(ITripRepository tripRepository)
    {
      _tripRepository = tripRepository;

      ClassLevelCascadeMode = CascadeMode.Stop;
      RuleLevelCascadeMode = CascadeMode.Stop;

      // TODO More validations
      RuleFor(p => p.FromDate)
        .NotEmpty()
        .WithMessage(new LocaleError(LocaleErrorKey.Required, [LocaleErrorParam.Trip]).ToJson());

      RuleFor(x => x.Note)
      .Must(x => x == null || !x.Contains('\''))
      .WithMessage(new LocaleError(LocaleErrorKey.InvalidInput, [LocaleErrorParam.Trip]).ToJson());
    }
  }
}