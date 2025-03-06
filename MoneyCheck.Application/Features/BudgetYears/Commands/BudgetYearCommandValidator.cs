using FluentValidation;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Models.Localization;
using System.Text.Json;

namespace MoneyCheck.Application.Features.BudgetYears.Commands
{
  public class BudgetYearCommandValidator : AbstractValidator<BudgetYearBase>
  {
    private readonly IBudgetYearRepository _budgetYearRepository;

    public BudgetYearCommandValidator(IBudgetYearRepository budgetYearRepository)
    {
      _budgetYearRepository = budgetYearRepository;

      ClassLevelCascadeMode = CascadeMode.Stop;
      RuleLevelCascadeMode = CascadeMode.Stop;

      // TODO More validations
      RuleFor(p => p.Year)
        .NotEmpty().WithMessage(new LocaleError(LocaleErrorKey.Required, [LocaleErrorParam.BudgetYear]).ToJson());
    }
  }
}