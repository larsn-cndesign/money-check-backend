using FluentValidation;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Categories.Commands
{
  public class CategoryCommandValidator : AbstractValidator<CategoryBase>
  {
    private readonly ICategoryRepository _categoryRepository;

    public CategoryCommandValidator(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;

      ClassLevelCascadeMode = CascadeMode.Stop;
      RuleLevelCascadeMode = CascadeMode.Stop;

      // TODO More validations
      RuleFor(p => p.CategoryName)
        .NotEmpty().WithMessage(new LocaleError(LocaleErrorKey.Required, LocaleErrorParam.Category).ToJson());
    }
  }
}