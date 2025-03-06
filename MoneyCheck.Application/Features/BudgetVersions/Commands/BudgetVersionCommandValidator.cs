using FluentValidation;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Models.Localization;
using System.Text.Json;

namespace MoneyCheck.Application.Features.BudgetVersions.Commands
{
  public class BudgetVersionCommandValidator : AbstractValidator<BudgetVersionBase>
  {
    private readonly IBudgetVersionRepository _budgetVersionRepository;

    public BudgetVersionCommandValidator(IBudgetVersionRepository budgetVersionRepository)
    {
      _budgetVersionRepository = budgetVersionRepository;

      ClassLevelCascadeMode = CascadeMode.Stop;
      RuleLevelCascadeMode = CascadeMode.Stop;

      // TODO Add validations
    }
  }
}

