using FluentValidation;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Models.Localization;
using System.Text.Json;

namespace MoneyCheck.Application.Features.BudgetItems.Commands
{
  public class BudgetItemCommandValidator : AbstractValidator<BudgetItemBase>
  {
    private readonly IBudgetItemRepository _budgetItemRepository;

    public BudgetItemCommandValidator(IBudgetItemRepository budgetItemRepository)
    {
      _budgetItemRepository = budgetItemRepository;

      ClassLevelCascadeMode = CascadeMode.Stop;
      RuleLevelCascadeMode = CascadeMode.Stop;
    }
  }
}